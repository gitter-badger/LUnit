using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Reflection;
using JetBrains.Annotations;
using LCore.Extensions;
using LCore.Interfaces;

// ReSharper disable VirtualMemberNeverOverriden.Global

namespace LCore.LUnit.Markdown
    {
    /// <summary>
    /// Implement this class to generate code for your assemblies and projects
    /// </summary>
    public abstract class MarkdownGenerator
        {
        /// <summary>
        /// Default string to tag for language, (C#)
        /// </summary>
        public const string CSharpLanguage = "cs";

        /// <summary>
        /// Override this member to specify the assemblies to generae documentation.
        /// </summary>
        protected abstract Assembly[] DocumentAssemblies { get; }

        #region Variables + 

        /// <summary>
        /// Other titled markdown,
        /// Root readme, table of contents, coverage summary, 
        /// custom documents, etc.
        /// </summary>
        protected Dictionary<string, GitHubMarkdown> Markdown_Other { get; } = new Dictionary<string, GitHubMarkdown>();

        /// <summary>
        /// Assembly-generated markdown documents.
        /// </summary>
        protected Dictionary<Assembly, GitHubMarkdown> Markdown_Assembly { get; } =
            new Dictionary<Assembly, GitHubMarkdown>();

        /// <summary>
        /// Type-generated markdown documents.
        /// </summary>
        protected Dictionary<Type, GitHubMarkdown> Markdown_Type { get; } = new Dictionary<Type, GitHubMarkdown>();

        /// <summary>
        /// Member-generated markdown documents.
        /// </summary>
        protected Dictionary<MemberInfo[], GitHubMarkdown> Markdown_Member { get; } =
            new Dictionary<MemberInfo[], GitHubMarkdown>();


        #endregion

        #region Generators + 

        // TODO Generate root markdown
        /// <summary>
        /// Generates root markdown document (front page)
        /// </summary>
        protected virtual GitHubMarkdown GenerateRootMarkdown()
            {
            var MD = new GitHubMarkdown(this, this.MarkdownPath_Root, this.MarkdownTitle_MainReadme);

            MD.Line(this.MarkdownTitle_MainReadme);

            this.Markdown_Assembly.Each(Document =>
            {
                MD.Line($" - {MD.Link(MD.GetRelativePath(Document.Value.FilePath), Document.Value.Title)}");
            });

            this.WriteFooter(MD);

            return MD;
            }

        /// <summary>
        /// Generates table of contents document
        /// </summary>
        protected virtual GitHubMarkdown GenerateTableOfContentsMarkdown()
            {
            var MD = new GitHubMarkdown(this, this.MarkdownPath_TableOfContents, this.MarkdownTitle_TableOfContents);

            MD.Header(this.MarkdownTitle_TableOfContents, Size: 3);

            this.GetAllMarkdown().Each(Document =>
                {
                    MD.Line($" - {MD.Link(MD.GetRelativePath(Document.FilePath), Document.Title)}");
                });

            this.WriteFooter(MD);

            return MD;
            }

        // TODO Generate summary markdown
        /// <summary>
        /// Generates coverage summary document
        /// </summary>
        protected virtual GitHubMarkdown GenerateCoverageSummaryMarkdown()
            {
            var MD = new GitHubMarkdown(this, this.MarkdownPath_CoverageSummary, this.MarkdownTitle_CoverageSummary);

            MD.Header(this.MarkdownTitle_CoverageSummary);

            this.WriteFooter(MD);

            return MD;
            }

        /// <summary>
        /// Generates markdown for an Assembly
        /// </summary>
        protected virtual GitHubMarkdown GenerateMarkdown(Assembly Assembly)
            {
            var MD = new GitHubMarkdown(this, this.MarkdownPath_Assembly(Assembly), Assembly.GetName().Name);

            MD.Header($"{Assembly.GetName().Name}", Size: 3);

            // TODO add Assembly comments

            // TODO add Static classes

            // TODO add Object classes

            // ReSharper disable once RedundantNameQualifier
            this.Markdown_Type.Select(Type => Type.Key.GetAssembly()?.GetName().Name == Assembly.GetName().Name)
                .Each(MD2 =>
                {
                    MD.Line($" - {MD.Link(MD.GetRelativePath(this.MarkdownPath_Type(MD2.Key)), MD2.Key.Name)}");
                });

            this.WriteFooter(MD);

            return MD;
            }

        /// <summary>
        /// Generates markdown for a Type
        /// </summary>
        protected virtual GitHubMarkdown GenerateMarkdown(Type Type)
            {
            var MD = new GitHubMarkdown(this, this.MarkdownPath_Type(Type), Type.Name);

            var Comments = Type.GetComments();

            // TODO up link

            MD.Header($"{Type.Name}", Size: 3);

            string TypePath = Type.FindClassFile();

            if (!string.IsNullOrEmpty(TypePath))
                {
                MD.Line(MD.Link(MD.GetRelativePath(TypePath), $"View Source {MD.Glyph(GlyphIcon.zoom_in)}"));
                }

            // TODO view source

            if (!string.IsNullOrEmpty(Comments?.Summary))
                {
                MD.Header("Summary", Size: 6);
                MD.Line(Comments.Summary);
                }

            this.Markdown_Member.Select(Member => Member.Key.First()?.DeclaringType == Type)
                .Each(Member => MD.Link(MD.GetRelativePath(this.MarkdownPath_Member(Member.Key.First())),
                    $" - {Member.Key.First()?.Name}"));

            this.WriteFooter(MD);

            return MD;
            }

        /// <summary>
        /// Generates markdown for a group of Members
        /// </summary>
        protected virtual GitHubMarkdown GenerateMarkdown(MemberInfo[] MemberGroup)
            {
            var Member = MemberGroup.First();

            var MD = new GitHubMarkdown(this, this.MarkdownPath_Member(Member), Member.Name);

            // TODO up link
            MD.Header($"{Member.DeclaringType?.Name}", Size: 3);

            string TypePath = Member.DeclaringType.FindClassFile();

            if (!string.IsNullOrEmpty(TypePath))
                {
                MD.Line(MD.Link(MD.GetRelativePath(TypePath), $"View Source {MD.Glyph(GlyphIcon.zoom_in)}"));
                }

            MD.Header(Member.Name);

            if (Member is MethodInfo)
                {
                var Method = (MethodInfo)Member;
                var Coverage = new MethodCoverage(Method);
                var Comments = Method.GetComments();

                string Static = Method.IsStatic
                    ? "Static "
                    : "Instance";

                string StaticLower = Method.IsStatic
                    ? " static"
                    : "";

                // TODO: Add return type link
                string ReturnType = Method.ReturnType == typeof(void)
                    ? "void"
                    : Method.ReturnType.FullyQualifiedName();

                // TODO: Add parameter type link
                string Parameters = Method.GetParameters().Convert(Param =>
                    $"{Param.ParameterType.GetGenericName()} {Param.Name}").Combine(", ");

                List<string> Badges = this.GetBadges(MD, Coverage, Comments);

                MD.Line("");
                MD.Line(Badges.JoinLines(" "));
                MD.Line("");

                MD.Header($"{Static}Method", Size: 4);


                MD.Header($"public{StaticLower} {ReturnType} {Member.Name}({Parameters});", Size: 6);

                MD.Header("Summary", Size: 6);
                MD.Line(Comments?.Summary);

                if (Method.GetParameters().Length > 0)
                    {
                    MD.Header("Parameters", Size: 6);

                    var Table = new List<string[]>
                        {
                        new[] {"Parameter", "Optional", "Type", "Description"}
                        };

                    // TODO: Add parameter type link
                    MD.Line(MD.Highlight("__Add parameter type link__"));

                    Method.GetParameters().Each((ParamIndex, Param) =>
                    {
                        Table.Add(new[]
                            {
                            Param.Name,
                            Param.IsOptional
                                ? "Yes"
                                : "No",
                            Param.ParameterType.GetGenericName(),
                            Comments?.Parameters.GetAt(ParamIndex)?.Obj2
                            });
                    });

                    MD.Table(Table);
                    }

                MD.Header("Returns", Size: 4);

                // TODO: Add return type link

                MD.Header(Method.ReturnType == typeof(void)
                    ? "void"
                    : Method.ReturnType.GetGenericName(), Size: 6);

                MD.Line(Comments?.Returns);

                if (Comments?.Examples.Length > 0)
                    {
                    MD.Header("Examples", Size: 4);
                    Comments.Examples.Each(Example => MD.Code(new[] { Example }));
                    }

                // TODO: Add test coverage link

                // TODO: Add exception details

                // TODO: Add permission details
                }

            this.WriteFooter(MD);

            return MD;
            }

        #endregion

        #region Helpers +

        /// <summary>
        /// Writes the footer for your markdown document
        /// </summary>
        protected virtual void WriteCustomFooter(GitHubMarkdown MD)
            {
            // TODO: Add custom copyright

            MD.Table(new[]
                {
                new[]
                    {
                    this.HomeLink(MD),
                    this.TableOfContentsLink(MD),
                    ""
                    }
                }, IncludeHeader: false);
            }

        /// <summary>
        /// Retrieves a formatted link to the table of contents
        /// </summary>
        protected virtual string TableOfContentsLink(GitHubMarkdown MD)
            {
            return MD.Link(MD.GetRelativePath(this.MarkdownPath_TableOfContents), this.MarkdownTitle_TableOfContents);
            }

        /// <summary>
        /// Retrieves a formatted link to the home readme
        /// </summary>
        protected virtual string HomeLink(GitHubMarkdown MD)
            {
            return MD.Link(MD.GetRelativePath(this.MarkdownPath_Root), this.MarkdownTitle_MainReadme);
            }

        /// <summary>
        /// Override this method to generate custom documents for your project.
        /// </summary>
        protected virtual Dictionary<string, GitHubMarkdown> GetOtherDocuments()
            {
            return new Dictionary<string, GitHubMarkdown>();
            }

        /// <summary>
        /// Override this method to customize badges included in member generated markdown documents.
        /// </summary>
        protected virtual List<string> GetBadges([NotNull] GitHubMarkdown MD, [CanBeNull] MethodCoverage Coverage,
            [CanBeNull] ICodeComment Comments)
            {
            var Member = Coverage?.CoveringMember;

            var Out = new List<string>();

            if (Member != null)
                {
                string SourcePath = Member.DeclaringType?.FindClassFile();

                string MethodScope = Member.IsPublic ? "Public" : "Protected";

                if (Member.IsAbstract)
                    MethodScope = $"Abstract {MethodScope}";

                string TypeDescription = Member.IsStatic ? "Static Method" : $"{MethodScope} Method";
                var TypeColor = GitHubMarkdown.BadgeColor.LightGrey;


                Out.Add(MD.Badge("Type", TypeDescription, TypeColor));
                Out.Add(MD.Badge("Documented", Comments != null ? "Yes" : "No",
                    Comments != null ? GitHubMarkdown.BadgeColor.BrightGreen : GitHubMarkdown.BadgeColor.Red));
                if (this.DocumentUnitCoverage)
                    Out.Add(MD.Badge("Unit Tested", Coverage?.MemberTraitFound == true ? "Yes" : "No",
                        Coverage?.MemberTraitFound == true
                            ? GitHubMarkdown.BadgeColor.BrightGreen
                            : GitHubMarkdown.BadgeColor.LightGrey));
                if (this.DocumentAttributeCoverage)
                    Out.Add(MD.Badge("Attribute Tests", $"{Coverage?.AttributeCoverage ?? 0}",
                        (Coverage?.AttributeCoverage ?? 0) > 0u
                            ? GitHubMarkdown.BadgeColor.BrightGreen
                            : GitHubMarkdown.BadgeColor.LightGrey));

                if (SourcePath == null)
                    Out.Add(MD.Badge("Source code", "Unavailable", GitHubMarkdown.BadgeColor.Red));
                else
                    Out.Add(MD.Link(MD.GetRelativePath(SourcePath),
                        MD.Badge("Source code", "Available", GitHubMarkdown.BadgeColor.BrightGreen)));

                // TODO: Count assertions by scanning test code '.should' 'assert'

                // TODO: Add Test Status: Passing / Failing / Untested
                }
            return Out;
            }

        #endregion

        #region Options +

        private void WriteFooter(GitHubMarkdown MD)
            {
            // TODO: Add custom copyright
            this.WriteCustomFooter(MD);
            MD.Table(new[]
                {
                new[]
                    {
                    $"This markdown was generated by {MD.Link(LUnit.Urls.GitHubRepository_LUnit, nameof(LUnit))}, " +
                    $"powered by {MD.Link(LUnit.Urls.GitHubRepository_LCore, nameof(LCore))}",
                    ""
                    }
                }, IncludeHeader: false);
            }


        /// <summary>
        /// Root path of the current running solution (development ONLY)
        /// </summary>
        public virtual string GeneratedMarkdownRoot => L.Ref.GetSolutionRootPath();

        /// <summary>
        /// Readme file name, default is "README.md"
        /// </summary>
        protected virtual string MarkdownPath_RootFile => "README.md";
        /// <summary>
        /// Root readme full path
        /// </summary>
        public virtual string MarkdownPath_Root => $"{this.GeneratedMarkdownRoot}\\{this.MarkdownPath_RootFile}";

        /// <summary>
        /// Table of contents file name, default is "TableOfContents.md"
        /// </summary>
        protected virtual string MarkdownPath_TableOfContentsFile => "TableOfContents.md";

        /// <summary>
        /// Table of contents readme full path
        /// </summary>
        public virtual string MarkdownPath_TableOfContents
            => $"{this.GeneratedMarkdownRoot}\\{this.MarkdownPath_TableOfContentsFile}";

        /// <summary>
        /// Coverage summary file name, default is "CoverageSummary.md"
        /// </summary>
        protected virtual string MarkdownPath_CoverageSummaryFile => "CoverageSummary.md";

        /// <summary>
        /// Coverage summary readme full path
        /// </summary>
        public virtual string MarkdownPath_CoverageSummary
            => $"{this.GeneratedMarkdownRoot}\\{this.MarkdownPath_CoverageSummaryFile}";

        /// <summary>
        /// Documents folder, default is "docs"
        /// </summary>
        protected virtual string MarkdownPath_Documentation => "docs";

        /// <summary>
        /// Generates the document title for an Assembly
        /// </summary>
        public virtual string MarkdownPath_Assembly(Assembly Assembly) =>
            $"{Assembly.GetRootPath()}\\{Assembly.GetName().Name.CleanFileName()}.md";

        /// <summary>
        /// Generates the document title for a Type
        /// </summary>
        public virtual string MarkdownPath_Type(Type Type) =>
            $"{Type.Assembly.GetRootPath()}\\{this.MarkdownPath_Documentation}\\" +
            $"{Type.Name.CleanFileName()}.md";

        /// <summary>
        /// Generates the document title for a Member
        /// </summary>
        public virtual string MarkdownPath_Member(MemberInfo Member) =>
            $"{Member.GetAssembly().GetRootPath()}\\{this.MarkdownPath_Documentation}\\" +
            $"{Member.DeclaringType?.Name.CleanFileName()}_{Member.Name.CleanFileName()}.md";

        // TODO: Add IExcludeFromDocumentation
        /// <summary>
        /// Determines if a Type should be included in documentation
        /// </summary>
        protected virtual bool IncludeType(Type Type) =>
            !Type.HasAttribute<ExcludeFromCodeCoverageAttribute>(IncludeBaseClasses: true) &&
            !Type.HasAttribute<IExcludeFromMarkdownAttribute>();

        // TODO: Add IExcludeFromDocumentation
        /// <summary>
        /// Determines if a Member should be included in documentation
        /// </summary>
        protected virtual bool IncludeMember(MemberInfo Member) =>
            !Member.HasAttribute<ExcludeFromCodeCoverageAttribute>(IncludeBaseClasses: true) &&
            !Member.HasAttribute<IExcludeFromMarkdownAttribute>() &&
            !(Member is MethodInfo && ((MethodInfo)Member).IsPropertyGetterOrSetter()) &&
            Member.IsDeclaredMember() &&
            !(Member is ConstructorInfo);

        /// <summary>
        /// Main readme title, default is "Home"
        /// </summary>
        protected virtual string MarkdownTitle_MainReadme => "Home";

        /// <summary>
        /// Table of Contents readme title, default is "Table of Contents"
        /// </summary>
        protected virtual string MarkdownTitle_TableOfContents => "Table of Contents";

        /// <summary>
        /// Coverage Summary readme title, default is "Coverage Summary"
        /// </summary>
        protected virtual string MarkdownTitle_CoverageSummary => "Coverage Summary";

        protected virtual bool DocumentUnitCoverage => true;

        protected virtual bool DocumentAttributeCoverage => true;
        #endregion

        #region Private Methods +

        private void Load(Assembly Assembly)
            {
            Assembly.GetExportedTypes().Select(this.IncludeType).Each(this.Load);

            this.Markdown_Assembly.Add(Assembly, this.GenerateMarkdown(Assembly));
            }

        private void Load(Type Type)
            {
            Dictionary<string, List<MemberInfo>> MemberNames = Type.GetMembers()
                .Select(this.IncludeMember)
                .Group(Member => Member.Name);

            MemberNames.Values.Convert(EnumerableExt.Array).Each(this.Load);

            this.Markdown_Type.Add(Type, this.GenerateMarkdown(Type));
            }

        private void Load(MemberInfo[] MemberGroup)
            {
            this.Markdown_Member.Add(MemberGroup, this.GenerateMarkdown(MemberGroup));
            }

        #endregion

        /// <summary>
        /// Generates all markdown documentation, optionally writing all files to disk using <paramref name="WriteToDisk"/>. 
        /// </summary>
        public void Generate(bool WriteToDisk = false)
            {
            // Generates all assembly, type, and member Markdown
            this.DocumentAssemblies.Each(this.Load);

            // Generate root markdown
            this.Markdown_Other.Add(this.MarkdownTitle_MainReadme, this.GenerateRootMarkdown());

            // Generate coverage summary markdown
            this.Markdown_Other.Add(this.MarkdownTitle_CoverageSummary, this.GenerateCoverageSummaryMarkdown());

            // Generate any custom markdown
            this.GetOtherDocuments().Each(Document => this.Markdown_Other.Add(Document.Key, Document.Value));

            // Lastly, generate table of contents
            this.Markdown_Other.Add(this.MarkdownTitle_TableOfContents, this.GenerateTableOfContentsMarkdown());

            List<GitHubMarkdown> AllMarkdown = this.GetAllMarkdown();

            if (WriteToDisk)
                {
                AllMarkdown.Each(MD =>
                {
                    string Path = MD.FilePath;

                    // just to be safe
                    if (Path.EndsWith(".md"))
                        {
                        Path.EnsurePathExists();

                        File.WriteAllLines(Path, MD.GetMarkdownLines().Array());
                        }
                });
                }
            }

        /// <summary>
        /// Gets all markdown generated by the generator.
        /// </summary>
        public List<GitHubMarkdown> GetAllMarkdown()
            {
            var AllMarkdown = new List<GitHubMarkdown>();

            AllMarkdown.AddRange(this.Markdown_Other.Values);
            AllMarkdown.AddRange(this.Markdown_Assembly.Values);
            AllMarkdown.AddRange(this.Markdown_Type.Values);
            AllMarkdown.AddRange(this.Markdown_Member.Values);

            return AllMarkdown;
            }
        }
    }