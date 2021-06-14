using System;
using System.Collections.Generic;

using Xunit;

using BlurbTemplate.Implementation.Tests.Utils;

namespace BlurbTemplate.Implementation.Tests
{
    public class WordReplacerTests
    {
        #region Simple Functional Tests

        [Theory]
        [InlineData("<AAAA> BBBB CCCC DDDD", "<AAAA>", "EEEE", "EEEE BBBB CCCC DDDD")]
        [InlineData("<AAAA> BBBB CCCC <AAAA> <AAAA>", "<AAAA>", "EEEE", "EEEE BBBB CCCC EEEE EEEE")]
        [InlineData("<AAAA> BBBB CCCC DDDD", "<BBBB>", "ZZZZ", "<AAAA> BBBB CCCC DDDD")]
        [InlineData("<AAAA> BBBB CCCC DDDD", "<AAAA>", "", " BBBB CCCC DDDD")]
        [InlineData("<AAAA> BBBB CCCC DDDD", "<AAAA>", null, " BBBB CCCC DDDD")]
        public void WordReplacer_ReplaceSinglePlaceholder_ShortTemplate_With_SinglePlaceholder(string template, string placeholder, string replacement, string expected)
        {
            var newText = new WordReplacer().ReplaceSinglePlaceholder(template, placeholder, replacement);
                        
            Assert.Equal(expected, newText);
        }


        [Theory]
        [InlineData("{AAAA} BBBB CCCC DDDD", "AAAA", "EEEE", "EEEE BBBB CCCC DDDD", "{", "}")]
        [InlineData("[[AAAA]] BBBB CCCC [[AAAA]] [[AAAA]]", "AAAA", "EEEE", "EEEE BBBB CCCC EEEE EEEE", "[[", "]]")]
        [InlineData("AAAA BBBB CCCC DDDD", "<BBBB>", "ZZZZ", "AAAA BBBB CCCC DDDD", "", "")]
        [InlineData("AAAA BBBB CCCC DDDD", "AAAA", "", " BBBB CCCC DDDD", "", "")]
        [InlineData("<-AAAA-> BBBB CCCC DDDD", "AAAA", null, " BBBB CCCC DDDD", "<-", "->")]
        public void WordReplacer_ReplaceSinglePlaceholder_ShortTemplate_With_SinglePlaceholder_CustomSeparators(string template, string placeholder, string replacement, string expected, string separatorStart, string separatorEnd)
        {
            var newText = new WordReplacer().ReplaceSinglePlaceholder(template, placeholder, replacement, separatorStart, separatorEnd);

            Assert.Equal(expected, newText);
        }


        [Theory]
        [InlineData("BBBB CCCC <AAAA>", "<AAAA>", "EEEE", "BBBB CCCC EEEE")]
        [InlineData("<AAAA> BBBB CCCC <AAAA>", "<AAAA>", "EEEE", "EEEE BBBB CCCC EEEE")]
        [InlineData("BBBB CCCC <AAAA>", "<BBBB>", "ZZZZ", "BBBB CCCC <AAAA>")]
        [InlineData("BBBB CCCC <AAAA>", "<AAAA>", "", "BBBB CCCC ")]
        [InlineData("BBBB CCCC <AAAA>", "<AAAA>", null, "BBBB CCCC ")]
        public void WordReplacer_ReplaceMultiplePlaceholders_ShortTemplate_With_SinglePlaceholder(string template, string placeholder, string replacement, string expected)
        {
            var newText = new WordReplacer().ReplaceMultiplePlaceholders(template, new Dictionary<string, string>() { { placeholder, replacement } });

            Assert.Equal(expected, newText);
        }

        public static IEnumerable<object[]> Template_PlaceholdersWithReplacementsPairs_Expected_Data
            => new List<object[]>
            {
                new object[] { "<AAAA> BBBB CCCC <DDDD> <AAAA>", new Dictionary<string, string>() { { "<AAAA>", "EEEE" }, { "<DDDD>", "FFFF" } }, "EEEE BBBB CCCC FFFF EEEE" },
                new object[] { "<AAAA> BBBB CCCC <DDDD> <AAAA>", new Dictionary<string, string>() { { "<A>", "E" }, { "<D>", "F" } }, "<AAAA> BBBB CCCC <DDDD> <AAAA>" },
                new object[] { "<AAAA> BBBB CCCC <DDDD> <AAAA>", new Dictionary<string, string>() { { "<AAAA>", "" }, { "<DDDD>", "" } }, " BBBB CCCC  " },
                new object[] { "<AAAA> BBBB CCCC <DDDD> <AAAA>", new Dictionary<string, string>() { { "<AAAA>", null }, { "<DDDD>", null } }, " BBBB CCCC  " }
            };

        [Theory]
        [MemberData(nameof(Template_PlaceholdersWithReplacementsPairs_Expected_Data))]
        public void WordReplacer_ReplaceMultiplePlaceholders_ShortTemplate_With_MultiplePlaceholders(string template, Dictionary<string, string> placeholdersWithReplacementsPairs, string expected)
        {            
            var newText = new WordReplacer().ReplaceMultiplePlaceholders(template, placeholdersWithReplacementsPairs);
            
            Assert.Equal(expected, newText);
        }

        #endregion Simple Functional Tests

        #region Tests with random text templates

        [Theory]
        [InlineData(1000, "<AAAA>", "EEEE")]
        [InlineData(2000, "<AAAA>", "EEEE")]
        [InlineData(4000, "<AABBAABBAABBAABBAABBAABB>", "EEEEFFFFEEEEFFFFEEEEFFFF")]
        [InlineData(2000, "<AAAA>", "")]
        public void WordReplacer_ReplaceMultiplePlaceholders_MediumTemplate_SinglePlaceholder(int numberOfWords, string placeholder, string replacement)
        {
            var (randomTemplate, sbWithReplacements) = RandomTextGenerator.GetRandomTextWithSinglePlaceholder(numberOfWords, (placeholder, replacement));

            var newText = new WordReplacer().ReplaceMultiplePlaceholders(randomTemplate, new Dictionary<string, string>() { { placeholder, replacement } });

            Assert.Equal(sbWithReplacements.ToString(), newText);           
        }

        #endregion Tests with random text templates

        #region Throw Exception Tests

        [Theory]
        [InlineData(null, "AAAA", "EEEE", "<", ">")]
        [InlineData("", "AAAA", "EEEE", "<", ">")]
        [InlineData(null, "AAAA", "EEEE", null, ">")]
        [InlineData("", "AAAA", "EEEE", "<", null)]
        public void WordReplacer_ReplaceSinglePlaceholder_ThrowsArgumentNullException(string template, string placeholder, string replacement, string separatorStart, string separatorEnd)
        {
            Assert.Throws<ArgumentNullException>(() => new WordReplacer().ReplaceMultiplePlaceholders(template, new Dictionary<string, string>() { { placeholder, replacement } }, separatorStart, separatorEnd));
        }

        [Fact]
        public void WordReplacer_ReplaceMultiplePlaceholders_NullPlaceholdersWithReplacementsPairs_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new WordReplacer().ReplaceMultiplePlaceholders("<AAAA>", null));
        }

        [Fact]
        public void WordReplacer_ReplaceSinglePlaceholder_TemplateBiggerThanLimit_ThrowsArgumentOutOfRangeException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new WordReplacer().ReplaceSinglePlaceholder(new string('c', 50001), "<AAAA>", "EEEE"));
        }


        #endregion Throw Exception Tests        
    }
}
