using Axis.Luna.Extensions;
using Axis.Pulsar.Grammar;
using Axis.Pulsar.Grammar.CST;
using Axis.Pulsar.Grammar.Recognizers.Results;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CodilityTests.Utils
{
    internal class VelocityExpressionExtractor
    {
        internal static (Exception, IVelocityExpression)[] ValidateTemplate(
            BufferedTokenReader reader,
            JObject json)
        {
            var expressions = ExtractExpressions(reader);
            var errors = new List<(Exception, IVelocityExpression)>();

            foreach(var expression in expressions)
            {
                try
                {
                    var evaluatable = expression.As<IEvaluatableExpression<JToken>>();
                    var result = evaluatable.Evaluate(json);
                }
                catch(Exception e)
                {
                    errors.Add((e, expression));
                }
            }

            return errors.ToArray();
        }

        internal static IVelocityExpression[] ExtractExpressions(BufferedTokenReader reader)
        {
            var expressions = new List<IVelocityExpression>();
            while (reader.TryNextToken(out var token))
            {
                if (token is '$')
                {
                    var position = reader.Position;
                    var recognizerResult = GrammarUtil.Grammar
                        .RootRecognizer()
                        .Recognize(reader.Back());

                    var exp = recognizerResult switch
                    {
                        SuccessResult success => ParseExpression(success.Symbol),
                        _ => throw new FormatException($"Could not parse the expression at: {position}")
                    };

                    expressions.Add(exp);
                }
            }

            return expressions.ToArray();
        }

        internal static IVelocityExpression ParseExpression(CSTNode expressionNode)
        {
            return expressionNode
                .AllChildNodes()
                .Aggregate(new Root().As<IVelocityExpression>(), (@base, next) =>
                {
                    return next.SymbolName switch
                    {
                        "root-accessor" => ParseRootAccessor(@base.As<IEvaluatableExpression<JToken>>(), next),
                        "get-accessor" => ParseGetAccessor(@base.As<IEvaluatableExpression<JToken>>(), next),
                        "dot-accessor" => ParseDotAccessor(@base.As<IEvaluatableExpression<JToken>>(), next),
                        "indexer" => ParseIndexerAccessor(@base.As<IEvaluatableExpression<JToken>>(), next),
                        _ => throw new FormatException($"Invalid symbol: {next.SymbolName}")
                    };
                });
        }

        internal static IVelocityExpression ParseRootAccessor(
            IEvaluatableExpression<JToken> map,
            CSTNode rootAccessorNode)
        {
            var identifier = ParseIdentifier(rootAccessorNode.FindNodes("identifier").First());
            return new PropertyAccessor
            {
                Map = map,
                Property = identifier
            };
        }

        internal static IVelocityExpression ParseGetAccessor(
            IEvaluatableExpression<JToken> map,
            CSTNode getAccessorNode)
        {
            var identifier = ParseStringIdentifier(getAccessorNode.FindNodes("string-identifier").First());
            return new GetAccessor
            {
                Map = map,
                Property = identifier
            };
        }

        internal static IVelocityExpression ParseDotAccessor(
            IEvaluatableExpression<JToken> map,
            CSTNode dotAccessorNode)
        {
            var identifier = ParseIdentifier(dotAccessorNode.FindNodes("identifier").First());
            return new PropertyAccessor
            {
                Map = map,
                Property = identifier
            };
        }

        internal static IVelocityExpression ParseIndexerAccessor(
            IEvaluatableExpression<JToken> map,
            CSTNode indexerAccessorNode)
        {
            var index = ParseIndex(indexerAccessorNode.FindNodes("index").First());
            return new IndexerAccessor
            {
                Base = map,
                Index = index
            };
        }


        internal static IVelocityExpression ParseIndex(CSTNode indexNode)
        {
            var node = indexNode.FirstNode();
            return node.SymbolName switch
            {
                "integer" => new IntegerIndex { Index = int.Parse(node.TokenValue()) },
                "string-identifier" => new StringIndex { Index = ParseStringIdentifier(node) },
                "expression" => new ExpressionIndex 
                {
                    Index = ParseExpression(node).As<IEvaluatableExpression<JToken>>()
                },
                _ => throw new FormatException($"Invalid symbol: {node.SymbolName}")
            };
        }

        internal static string ParseIdentifier(
            CSTNode identifierNode)
            => identifierNode.TokenValue();

        internal static string ParseStringIdentifier(
            CSTNode stringIdentifierNode)
            => ParseIdentifier(stringIdentifierNode.FindNodes("identifier").First());
    }

    internal interface IVelocityExpression
    {
    }

    internal interface IEvaluatableExpression<out JResult> : IVelocityExpression
    where JResult : JToken
    {
        JResult Evaluate(JToken value);
    }

    internal record IntegerIndex : IEvaluatableExpression<JToken>
    {
        public int Index { get; set; }

        public JToken Evaluate(JToken value) => new JValue(Index);

        public override string ToString() => Index.ToString();
    }

    internal record StringIndex : IEvaluatableExpression<JToken>
    {
        public string Index { get; set; }

        public JToken Evaluate(JToken value) => new JValue(Index);

        public override string ToString() => $"\"{Index}\"";
    }

    internal record ExpressionIndex: IEvaluatableExpression<JValue>
    {
        public IEvaluatableExpression<JToken> Index { get; set; }

        public JValue Evaluate(JToken value)
        {
            var token = Index.Evaluate(value);
            return token.Type == JTokenType.Integer
                ? (JValue)token
                : throw new InvalidOperationException($"the index evaluates to the wrong type: {token.Type}");
        }

        public override string ToString() => Index.ToString();
    }

    internal record Root : IEvaluatableExpression<JToken>
    {
        public JToken Evaluate(JToken value) => value;

        public override string ToString() => "$";
    }

    internal record PropertyAccessor : IEvaluatableExpression<JToken>
    {
        public IEvaluatableExpression<JToken> Map { get; set; }

        public string Property { get; set; }

        public JToken Evaluate(JToken value)
        {
            return Map.Evaluate(value)[Property]!;
        }

        public override string ToString()
        {
            if (Map is Root)
                return $"{Map}{Property}";

            else return $"{Map}.{Property}";
        }
    }

    internal record GetAccessor : IEvaluatableExpression<JToken>
    {
        public IEvaluatableExpression<JToken> Map { get; set; }

        public string Property { get; set; }

        public JToken Evaluate(JToken value)
        {
            return Map.Evaluate(value)[Property]!;
        }

        public override string ToString()
        {
            if (Map is Root)
                return $"get(\"{Property}\")";

            else return $"{Map}.get(\"{Property}\")";
        }
    }

    internal record IndexerAccessor: IEvaluatableExpression<JToken>
    {
        public IEvaluatableExpression<JToken>  Base { get; set; }

        public IVelocityExpression Index { get; set; }

        public JToken Evaluate(JToken value)
        {
            var instance = Base.Evaluate(value);
            return (instance, Index) switch
            {
                (JArray array, IntegerIndex index) => array[index.Index],
                (JObject obj, StringIndex index) => obj[index.Index],
                (_, ExpressionIndex index) => (instance, index.Index.Evaluate(value)) switch
                {
                    (JArray array, JValue tindex) => array[(int) tindex.Value],
                    (JObject obj, JValue tindex) => obj[(string) tindex.Value],
                    _ => throw new InvalidOperationException($"Invalid index found: '{Index?.GetType()}'")
                },
                _ => throw new InvalidOperationException($"Invalid index found: '{Index?.GetType()}'")
            };
        }

        public override string ToString()
        {
            return $"{Base}[{Index}]";
        }
    }
}
