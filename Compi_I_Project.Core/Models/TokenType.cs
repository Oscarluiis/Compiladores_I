﻿using System;
namespace Compi_I_Project.Core.Models
{
        public enum TokenType
        {
            Number,
            Boolean,
            String,
            Array,
            KwIf,
            KwElse,
            KwFor,
            KwWhile,
            KwIn,
            KwdDo,
            KwPuts,
            KwEnd,
            OpPlus,
            OpMinus,
            OpMul,
            OpDiv,
            OpMod,
            OpEOF,
            Unknown,
            Identifier,
            OpLessOrEqualThan,
            OpLessThan,
            OpGreaterOrEqualThan,
            OpGreaterThan,
            OpColon,
            OpSemicolon,
            OpLeftParens,
            OpRightParens,
            OpLeftBracket,
            OpRightBracket,
            OpLeftBrace,
            OpRightBrace,
            OpComma,
            OpEqual,
            OpAssignation,
            OpStringLiteral,
            OpLogicalAnd,
            OpLogicalOr,

        }
}
