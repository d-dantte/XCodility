using Axis.Pulsar.Grammar.Language;
using Axis.Pulsar.Languages.xBNF;

namespace CodilityTests.Utils
{
    internal static class GrammarUtil
    {
        public static Grammar Grammar { get; }


        static GrammarUtil()
        {
            using var stream = typeof(GrammarUtil).Assembly.GetManifestResourceStream(
                "CodilityTests.Utils.VelocityExpressionGrammar.xbnf");

            var importer = new Importer();
            Grammar = importer.ImportGrammar(stream);
        }
    }
}
