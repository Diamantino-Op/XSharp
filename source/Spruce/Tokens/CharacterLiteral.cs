﻿using System;

namespace Spruce.Tokens
{
    public class CharacterLiteral : Token
    {
        // We are overriding the parse method to set rStart itself
        public override object Parse(string aText, ref int rStart)
        {
            int i;

            var isEscaped = false;
            var foundEndQuote = false;

            for (i = 1; i < aText.Length - rStart; i++)
            {
                if (aText[rStart + i] == '\\')
                {
                    isEscaped = true;
                    continue;
                }

                if (aText[rStart + i] == '\'' && !isEscaped)
                {
                    foundEndQuote = true;
                    i += 1; // Increment i so that the end quote is also included
                    break;
                }

                isEscaped = false;
            }

            // Cannot find end quote. We cannot parse the object as string.
            if (!foundEndQuote)
                return null;

            // Replace all \' with ' and then remove the opening quotes
            var xResult = aText.Substring(rStart + 1, i - 2).Replace(@"\'", @"'");
            if (xResult.Length != 1)
            {
                throw new ArgumentOutOfRangeException("CharacterLiteral should only be a single character.");
            }
            rStart += i;
            return xResult;
        }
    }
}
