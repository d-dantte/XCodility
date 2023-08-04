using Axis.Pulsar.Grammar;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodilityTests.Utils
{
    internal class VelocityExpressionExtractor
    {
        public static IVelocityExpression[] ExtractExpressions(BufferedTokenReader reader)
        {
            while (reader.TryNextToken(out var token))
            {
                if(token is '$')
            }
        }
    }

    internal interface IVelocityExpression
    {
    }

    internal interface IEvaluatableExpression<JResult> : IVelocityExpression
    where JResult : JToken
    {
        JResult Evaluate(JToken value);
    }

    internal record IntegerIndex : IEvaluatableExpression<JToken>
    {
        public int Index { get; set; }

        public JToken Evaluate(JToken value) => new JValue(Index);
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
    }

    internal record Instance : IEvaluatableExpression<JToken>
    {
        public JToken Evaluate(JToken value) => value;
    }

    internal record PropertyAccessor: IEvaluatableExpression<JToken>
    {
        public IEvaluatableExpression<JObject> Map { get; set; }

        public string Property { get; set; }

        public JToken Evaluate(JToken value)
        {
            return Map.Evaluate(value)[Property]!;
        }
    }

    internal record GetPropertyAccessor : IEvaluatableExpression<JToken>
    {
        public IEvaluatableExpression<JObject> Map { get; set; }

        public string Property { get; set; }

        public JToken Evaluate(JToken value)
        {
            return Map.Evaluate(value)[Property]!;
        }
    }

    internal record Indexer: IEvaluatableExpression<JToken>
    {
        public IVelocityExpression Array { get; set; }

        public int Index { get; set; }

        public JToken Evaluate(JToken value)
        {
            throw new NotImplementedException();
        }
    }
}
