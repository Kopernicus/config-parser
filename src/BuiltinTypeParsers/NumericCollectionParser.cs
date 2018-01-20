﻿/**
 * Kopernicus ConfigNode Parser
 * ====================================
 * Created by: Teknoman117 (aka. Nathaniel R. Lewis)
 * Maintained by: Thomas P.
 * -------------------------------------------------------------
 * This library is free software; you can redistribute it and/or
 * modify it under the terms of the GNU Lesser General Public
 * License as published by the Free Software Foundation; either
 * version 3 of the License, or (at your option) any later version.
 *
 * This library is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
 * Lesser General Public License for more details.
 *
 * You should have received a copy of the GNU Lesser General Public
 * License along with this library; if not, write to the Free Software
 * Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston,
 * MA 02110-1301  USA
 *
 * This library is intended to be used as a plugin for Kerbal Space Program
 * which is copyright 2011-2016 Squad. Your usage of Kerbal Space Program
 * itself is governed by the terms of its EULA, not the license above.
 *
 * https://kerbalspaceprogram.com
 */

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace Kopernicus
{
    /// <summary>
    /// Simple parser for numeric collections 
    /// </summary>
    [RequireConfigType(ConfigType.Value)]
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    public class NumericCollectionParser<T> : IParsable, ITypeParser<List<T>>
    {
        /// <summary>
        /// The value that is being parsed
        /// </summary>
        public List<T> Value { get; set; }
        
        /// <summary>
        /// The method that is used to parse the string
        /// </summary>
        private readonly MethodInfo _parserMethod;
        
        /// <summary>
        /// Parse the Value from a string
        /// </summary>
        public void SetFromString(String s)
        {
            // Need a new list
            Value = new List<T>();

            // Get the tokens of this String
            foreach (String e in s.Split(' '))
            {
                Value.Add((T)_parserMethod.Invoke(null, new Object[] { e }));
            }
        }
        
        /// <summary>
        /// Create a new NumericCollectionParser
        /// </summary>
        public NumericCollectionParser()
        {
            // Get the parse method for this object
            _parserMethod = typeof(T).GetMethod("Parse", new [] { typeof(String) });
        }
        
        /// <summary>
        /// Create a new NumericCollectionParser from already existing values
        /// </summary>
        public NumericCollectionParser(List<T> i) : this()
        {
            Value = i;
        }

        /// <summary>
        /// Convert Parser to Value
        /// </summary>
        public static implicit operator List<T>(NumericCollectionParser<T> parser)
        {
            return parser.Value;
        }
        
        /// <summary>
        /// Convert Value to Parser
        /// </summary>
        public static implicit operator NumericCollectionParser<T>(List<T> value)
        {
            return new NumericCollectionParser<T>(value);
        }
    }
}