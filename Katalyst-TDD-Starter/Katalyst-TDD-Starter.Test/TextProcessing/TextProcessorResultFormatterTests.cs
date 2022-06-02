﻿using Katalyst_TDD_Starter.TextProcessing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Katalyst_TDD_Starter.Test.TextProcessing
{
    [TestClass]
    public class TextProcessorResultFormatterTests
    {
        public TextProcessorResultFormatter ToTest { get; private set; }

        public TextProcessorResultFormatterTests()
        {
            ToTest = new TextProcessorResultFormatter();
        }

        [TestMethod]
        [DataRow (1)]
        [DataRow(5)]
        [DataRow(10)]
        public void The_output_should_begin_with_the_top_words_message(int mostUsedWordsCount)
        {
            var mostUsedWords = new List<string>();

            for (int i = 0; i < mostUsedWordsCount; i++)
            {
                mostUsedWords.Add(string.Empty);
            }

            var input = new TextProcessorResult
            {
                MostUsedWords = mostUsedWords
            };

            var expected = $"These are the top {mostUsedWordsCount} words used:";
            var actual = ToTest.Format(input);

            Assert.IsTrue(actual.StartsWith(expected));
        }
    }
}
