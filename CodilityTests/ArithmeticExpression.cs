using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CodilityTests
{
    public struct Number
    {
        public object Value { get; private set; }

        public Number(long value)
        {
            Value = value;
        }

        public Number(ulong value)
        {
            Value = value;
        }

        public Number(decimal value)
        {
            Value = value;
        }

        public Number(double value)
        {
            Value = value;
        }

        public override string ToString() => Value.ToString();

        public static implicit operator Number(long value) => new Number(value);

        public static implicit operator Number(ulong value) => new Number(value);

        public static implicit operator Number(decimal value) => new Number(value);

        public static implicit operator Number(double value) => new Number(value);
    }

    public interface IExpression
    {
        //Number Evaluate();
    }

    public enum Operator
    {
        Add,
        Subtract,
        Multiply,
        Divide,
        Power
    }

    public record Operation
    {
        public IExpression Object { get; }

        public Operator Operator { get; }

        public Operation(Operator @operator, IExpression @object)
        {
            Operator = @operator;
            Object = @object ?? throw new ArgumentNullException(nameof(@object));
        }

        public override string ToString()
        {
            var symbol = Operator switch
            {
                Operator.Add => "+",
                Operator.Subtract => "-",
                Operator.Multiply => "*",
                Operator.Divide => "/",
                Operator.Power => "^",
                _ => throw new InvalidOperationException($"Invalid operator encountered: {Operator}")
            };

            return $"{symbol} {Object}";
        }
    }

    public record ConstantExpression: IExpression
    {
        public Number Number { get; }

        public ConstantExpression(Number number)
        {
            Number = number;
        }

        public override string ToString() => Number.ToString();

        public static implicit operator ConstantExpression(Number number) => new ConstantExpression(number);
    }

    public record ArithmeticExpression: IExpression
    {
        private List<Operation> operations = new List<Operation>();

        public IExpression Subject { get; }

        public bool IsBinaryOperation => operations.Count == 1;

        public bool IsUnaryOperation => operations.Count == 0;

        public ArithmeticExpression(IExpression subject)
        {
            Subject = subject ?? throw new ArgumentNullException(nameof(subject));
        }

        public ArithmeticExpression Add(IExpression expression)
        {
            operations.Add(new Operation(Operator.Add, expression));
            return this;
        }

        public ArithmeticExpression Subtract(IExpression expression)
        {
            operations.Add(new Operation(Operator.Subtract, expression));
            return this;
        }

        public ArithmeticExpression Multiply(IExpression expression)
        {
            operations.Add(new Operation(Operator.Multiply, expression));
            return this;
        }

        public ArithmeticExpression Divide(IExpression expression)
        {
            operations.Add(new Operation(Operator.Divide, expression));
            return this;
        }

        public ArithmeticExpression Power(IExpression expression)
        {
            operations.Add(new Operation(Operator.Power, expression));
            return this;
        }

        // modified
        public override string ToString()
        {
            if (operations.Count > 0)
            {
                var ops = string.Join(" ", operations.Select(op => op.ToString()));

                return $"({Subject} {ops})";
            }

            return $"({Subject})";
        }

        // modified
        public ArithmeticExpression Normalize()
        {
            var exp = operations.Aggregate(new ArithmeticExpression(Normalize(Subject)), (prev, next) =>
            {
                return prev.Append(new Operation(next.Operator, Normalize(next.Object)));
            });

            return Enum
                .GetValues<Operator>()
                .OrderByDescending(op => op)
                .Aggregate(exp, GroupByOperator);
        }

        // New function
        private static IExpression Normalize(IExpression expression)
        {
            return expression switch
            {
                ArithmeticExpression ae => ae.Normalize(),
                _ => expression
            };
        }

        // New function
        private ArithmeticExpression Append(Operation operation)
        {
            return operation.Operator switch
            {
                Operator.Add => Add(operation.Object),
                Operator.Subtract => Subtract(operation.Object),
                Operator.Multiply => Multiply(operation.Object),
                Operator.Divide => Divide(operation.Object),
                Operator.Power => Power(operation.Object),
                _ => throw new ArgumentException($"Invalid operator: {operation.Operator}")
            };
        }

        // New function
        private static bool Matches(
            Operation operation,
            Operator @operator)
            => operation.Operator == @operator;

        // New function
        private static ArithmeticExpression GroupByOperator(ArithmeticExpression expression, Operator @operator)
        {
            ArithmeticExpression @base = null;
            ArithmeticExpression homogeneousGroup = null;
            Operation previousOp = new Operation(Operator.Add, expression.Subject);

            foreach (var nextOp in expression.operations)
            {
                if (!Matches(nextOp, @operator))
                {
                    if (@base is null)
                    {
                        @base = new ArithmeticExpression(
                            homogeneousGroup is null
                            ? previousOp.Object
                            : ReduceToBinaryExpression(@operator, homogeneousGroup));
                    }
                    else
                    {
                        _ = @base.Append(
                            homogeneousGroup is null
                            ? previousOp
                            : new Operation(
                                previousOp.Operator,
                                ReduceToBinaryExpression(@operator, homogeneousGroup)));
                    }

                    previousOp = nextOp;
                    homogeneousGroup = null;
                }
                else
                {
                    if (homogeneousGroup is null)
                    {
                        homogeneousGroup = new ArithmeticExpression(previousOp.Object);
                        previousOp = new Operation(previousOp.Operator, homogeneousGroup);
                    }

                    _ = homogeneousGroup.Append(nextOp);
                }
            }

            if (@base is null)
            {
                @base = new ArithmeticExpression(
                    homogeneousGroup is null
                    ? previousOp.Object
                    : ReduceToBinaryExpression(@operator, homogeneousGroup));
            }
            else
            {
                _ = @base.Append(
                    homogeneousGroup is null
                    ? previousOp
                    : new Operation(
                        previousOp.Operator,
                        ReduceToBinaryExpression(@operator, homogeneousGroup)));
            }

            return @base;
        }

        // New function
        private static IExpression ReduceToBinaryExpression(Operator @operator, ArithmeticExpression homogeneousExpression)
        {
            var exp = @operator switch
            {
                Operator.Power => homogeneousExpression.operations
                    .Select(op => op.Object)
                    .Reverse()
                    .Append(homogeneousExpression.Subject)
                    .Aggregate(default(IExpression), (prev, next) =>
                    {
                        if (prev is null)
                        {
                            return next;
                        }

                        var op = new Operation(@operator, prev);
                        return new ArithmeticExpression(next).Append(op);
                    }),

                _ => Enumerable
                    .Empty<IExpression>()
                    .Append(homogeneousExpression.Subject)
                    .Concat(homogeneousExpression.operations.Select(op => op.Object))
                    .Aggregate(default(IExpression), (prev, next) =>
                    {
                        if (prev is null)
                        {
                            return next;
                        }

                        var op = new Operation(@operator, next);
                        return new ArithmeticExpression(prev).Append(op);
                    })
            };

            return exp;
        }

        #region Helperfunctions
        public static ArithmeticExpression New(Number subject) => new ArithmeticExpression(new ConstantExpression(subject));

        public ArithmeticExpression Add(
            Number expression)
            => Add(new ConstantExpression(expression));

        public ArithmeticExpression Subtract(
            Number expression)
            => Subtract(new ConstantExpression(expression));

        public ArithmeticExpression Multiply(
            Number expression)
            => Multiply(new ConstantExpression(expression));

        public ArithmeticExpression Divide(
            Number expression)
            => Divide(new ConstantExpression(expression));

        public ArithmeticExpression Power(
            Number expression)
            => Power(new ConstantExpression(expression));
        #endregion
    }

    /// <summary>
    /// Given an Arithmetic Expression, write an algorithm to normalize the expression - i.e, complete the ArithmeticExpression.Normalize() function.
    /// <para>
    /// A normalied ArithmeticExpression:
    /// 1. follows the BODMAS rules to group it's individual operations
    /// 2. Is a binary operation (contains only 1 item in it's operation list) or
    /// 3. Is a unary operation (contains an empty operation list)
    /// 
    /// Examples
    /// 1. (4 + 4 + 4 + 1) normalizes to (((4 + 4) + 4) + 1)
    /// 2. (5) normalizes to (5)
    /// 3. (4 + 5 + 6 - 3) normalizes to (((4 + 5) + 6) - 3)
    /// 4. (3 * 5 * 1 / 2 / 8 ^ 2 ^ 3 + 4 + 7 + 8 * 6) noramlizes to (((((((3 * 5 ) * (1 / 2) / (8 ^ ( 2 ^ 3))) + 4) + 7) + (8 * 6))
    /// </para>
    /// </summary>
    [TestClass]
    public class ArithmeticExpressionTest
    {
        [TestMethod]
        public void Tests()
        {
            var exp = ArithmeticExpression
                .New(3L)
                .Multiply(5L)
                .Multiply(1L)
                .Divide(2L)
                .Divide(8L)
                .Power(2L)
                .Power(3L)
                .Add(4l)
                .Add(7l)
                .Add(8l)
                .Multiply(6L);

            Console.WriteLine(exp);

            var normalized = exp.Normalize();
            Console.WriteLine(normalized);

            exp = ArithmeticExpression
                .New(3l)
                .Add(4l)
                .Add(5l)
                .Add(0l);
            Console.WriteLine(exp);
            normalized = exp.Normalize();
            Console.WriteLine(normalized);

            exp = ArithmeticExpression
                .New(32l)
                .Subtract(5l)
                .Multiply(exp)
                .Divide(1l);
            Console.WriteLine(exp);
            normalized = exp.Normalize();
            Console.WriteLine(normalized);

            exp = ArithmeticExpression.New(5456l);
            Console.WriteLine(exp);
            normalized = exp.Normalize();
            Console.WriteLine(normalized);
        }
    }
}
