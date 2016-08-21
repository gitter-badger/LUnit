using System;
using System.Collections;
using System.Collections.Generic;

namespace LCore.LUnit.Markdown
    {
    /// <summary>
    /// Excludes a class or member from being included in GitHub Markdown autogeneration.
    /// </summary>
    [AttributeUsage(AttributeTargets.All)]
    public class ExcludeFromMarkdownAttribute : Attribute, IExcludeFromMarkdownAttribute
        {

        }
    }