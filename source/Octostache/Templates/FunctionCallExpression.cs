﻿using System.Linq;

namespace Octostache.Templates
{
    /// <summary>
    /// Syntactically this appears as the <code>| FilterName</code> construct, where
    /// the (single) argument is specified to the left of the bar. Under the hood this
    /// same AST node will also represent classic <code>Function(Foo,Bar)</code> expressions.
    /// </summary>
    class FunctionCallExpression : ContentExpression
    {
        
        readonly bool _filterSyntax;

        public FunctionCallExpression(bool filterSyntax, string function, ContentExpression argument, params Identifier[] options)
        {
            Options = options;
            _filterSyntax = filterSyntax;
            Function = function;
            Argument = argument;
        }

        public Identifier[] Options { get; }
        public string Function { get; }

        public ContentExpression Argument { get; }

        public override string ToString()
        {
            if (_filterSyntax)
                return $"{Argument} | {Function}{(Options.Any() ? " " : "")}{string.Join(" ", Options.Select(t => t.ToString()))}";
                    

            return $"{Function} ({Argument}{(Options.Any() ? ", " : "")}{string.Join(", ", Options.Select(t => t.ToString()))})";
        }
    }
}
