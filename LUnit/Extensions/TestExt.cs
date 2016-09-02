﻿using System;
using System.Collections.Generic;
using LCore.Extensions;
using System.Collections;
// ReSharper disable once RedundantUsingDirective
using System.Diagnostics;
using System.Reflection;
using JetBrains.Annotations;
using LCore.LUnit.Assert;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xunit.Sdk;

// ReSharper disable UnusedMember.Global

namespace LCore.LUnit
    {
    /// <summary>
    /// Provides extensions to allow for method unit testing.
    /// </summary>
    public static class TestExt
        {
        #region GetTestMembers

        /// <summary>
        /// Retrieves TestAttributes for type <paramref name="Type" />
        /// </summary>
        [NotNull]
        public static Dictionary<MemberInfo, List<ILUnitAttribute>> GetTestMembers([CanBeNull] this Type Type)
            {
            var Tests = new Dictionary<MemberInfo, List<ILUnitAttribute>>();

            if (Type?.IsInterface == false && !Type.IsValueType)
                {
                Type.GetMembers().Each(Member =>
                    {
                    // Base Type members should not be returned. Only type-specific methods included for direct tests.
                    if (Member.DeclaringType != Type || Member is ConstructorInfo || Member is FieldInfo)
                        return;

                    if (!Tests.ContainsKey(Member))
                        Tests.Add(Member, new List<ILUnitAttribute>());

                    Member.GetAttributes<ILUnitAttribute>(IncludeBaseTypes: false)
                        .Each(Attr => { Tests[Member].Add(Attr); });
                    });
                }

            return Tests;
            }

        #endregion

        #region RunTest

        /// <summary>
        /// Execute an ITestResultAttribute and compare ActualResult with ExpectedResult.
        /// </summary>
        public static void RunTest(this ITestResultAttribute Attr, MethodInfo Method)
            {
            Func<object, bool>[] Checks = Attr.AdditionalResultChecks.Convert(
                L.F<MethodInfo, string, Func<object, bool>>(LUnit.GetCheckMethodArg).Supply(Method));

            //            Method.AssertResult(Parameters, ExpectedResult, Checks);

            //var Info = typeof(TestExt).GetMethods().First((Func<MethodInfo, bool>) (MethodInfo =>
            //    MethodInfo.Name == nameof(AssertionExt.AssertResult) &&
            //    MethodInfo.ContainsGenericParameters));

            //if (Info != null)
            //   {
            var ExpectedResult = Attr.ExpectedResult;
            object[] Parameters = Attr.Parameters;

            LUnit.FixParameterTypes(Method, Parameters);
            LUnit.FixObject(Method, Method.ReturnType, ref ExpectedResult);


            Method.AssertResult(Target: null, Params: Parameters, ExpectedResult: ExpectedResult,
                AdditionalResultChecks: Checks);

            //Info = Info.MakeGenericMethod(ExpectedResult.GetType());

            //Info.Invoke(null, new[] {Method, null, Parameters, ExpectedResult, Checks});
            //   }
            }

        /// <summary>
        /// Execute an ITestFailsAttribute test and ensure failure matches the conditions defined.
        /// </summary>
        public static void RunTest(this ITestFailsAttribute Attr, MethodInfo Method)
            {
            Func<bool>[] Checks =
                Attr.AdditionalChecks.Convert(L.F<MethodInfo, string, Func<bool>>(LUnit.GetCheckMethod).Supply(Method));

            Method.AssertFails(Attr.Parameters, Method.ReflectedType, Attr.ExceptionType, Checks);
            }

        /// <summary>
        /// Execute an ITestFailsAttribute test and ensures the method succeeds.
        /// </summary>
        public static void RunTest(this ITestSucceedsAttribute Attr, MethodInfo Method)
            {
            Func<bool>[] Checks =
                Attr.AdditionalChecks.Convert(L.F<MethodInfo, string, Func<bool>>(LUnit.GetCheckMethod).Supply(Method));

            object[] Parameters = Attr.Parameters;

            LUnit.FixParameterTypes(Method, Parameters);

            Method.AssertSucceedes(Target: null, Params: Parameters, AdditionalChecks: Checks);
            }

        /// <summary>
        /// Execute an ITestSourceAttribute test and perform tests on the calling object (for extension methods).
        /// </summary>
        public static void RunTest(this ITestSourceAttribute Attr, MethodInfo Method)
            {
            Func<object, bool>[] Checks =
                Attr.AdditionalSourceChecks.Convert(
                    L.F<MethodInfo, string, Func<object, bool>>(LUnit.GetCheckMethodArg).Supply(Method));

            //    Method.AssertSource(Parameters, ExpectedSource);

            var OutMethod = typeof(TestExt).GetMethods().First(MethodInfo =>
                    MethodInfo.Name == nameof(AssertionExt.AssertSource) && MethodInfo.ContainsGenericParameters);

            if (Attr.ExpectedSource != null)
                {
                OutMethod = OutMethod?.MakeGenericMethod(Attr.ExpectedSource.GetType());
                }
            else if (Attr.Parameters[0] != null)
                {
                OutMethod = OutMethod?.MakeGenericMethod(Attr.Parameters[0].GetType());
                }
            else
                {
                OutMethod = OutMethod?.MakeGenericMethod(typeof(object));
                }

            var ExpectedSource = Attr.ExpectedSource;
            object[] Parameters = Attr.Parameters;

            LUnit.FixParameterTypes(Method, Parameters);
            LUnit.FixObject(Method, Method.GetParameters()[0].ParameterType, ref ExpectedSource);

            OutMethod?.Invoke(obj: null, parameters: new[] {Method, null, Parameters, ExpectedSource, Checks});
            }

        /// <summary>
        /// Validates an IValidateAttribute, throwing a testing error if validation has any errors.
        /// </summary>
        public static void RunTest(this IValidateAttribute Attr, MemberInfo Member)
            {
            string[] Out = Attr.Validate(Member);

            if (Out.IsEmpty())
                return;

            throw new InternalTestFailureException(
                $"Attribute validation failed: {Attr.GetType()} {Member.FullyQualifiedName()}\n{Out.JoinLines()}");
            }

        #endregion

        #region GetTargetingName

        /// <summary>
        /// Returns a Tuple of strings representing the:
        /// Namespace, Class Name, Member Name to target the specified MemberInfo.
        /// Use this to directly target members for testing.
        /// </summary>
        /// <param name="Member"></param>
        /// <param name="TestNamespaceFormat"></param>
        /// <param name="TestClassFormat"></param>
        /// <param name="TestMethodFormat"></param>
        /// <returns></returns>
        public static Tuple<string, string, string> GetTargetingName([CanBeNull] this MemberInfo Member,
            string TestNamespaceFormat = LUnit.Format.Namespace,
            string TestClassFormat = LUnit.Format.Class,
            string TestMethodFormat = LUnit.Format.Member)
            {
            if (Member == null)
                return null;

            var Replacements = new Dictionary<string, string>
                {
                ["`"] = "_",
                ["["] = "",
                ["]"] = "",
                ["&"] = "",
                ["."] = "_",
                ["("] = "_",
                [")"] = "_",
                [","] = "_",
                [" "] = "_",
                ["<"] = "_",
                [">"] = "_",
                ["="] = "_",
                ["__"] = "_"
                };

            if (Member is Type)
                return new Tuple<string, string, string>(
                    string.Format(TestNamespaceFormat, ((Type) Member).GetAssembly()?.GetName().Name,
                        ((Type) Member).Namespace),
                    string.Format(TestClassFormat, ((Type) Member).GetNestedNames())
                        .ReplaceAll(Replacements),
                    "");

            if (Member.DeclaringType == null)
                return null;


            MemberInfo[] DuplicateMembers = Member.DeclaringType.GetMember(Member.Name);


            if (DuplicateMembers.Length == 1)
                {
                return new Tuple<string, string, string>(
                    string.Format(TestNamespaceFormat, Member.GetAssembly()?.GetName().Name, Member.GetNamespace()),
                    string.Format(TestClassFormat, Member.DeclaringType?.GetNestedNames(), "")
                        .ReplaceAll(Replacements),
                    string.Format(TestMethodFormat, Member.Name)
                        .ReplaceAll(Replacements));
                }

            if (DuplicateMembers.Length > 1)
                {
                // differing parameters
                if (Member is MethodInfo)
                    {
                    return new Tuple<string, string, string>(
                        string.Format(TestNamespaceFormat, Member.GetAssembly()?.GetName().Name, Member.GetNamespace()),
                        string.Format(TestClassFormat, Member.DeclaringType?.GetNestedNames(), "")
                            .ReplaceAll(Replacements),
                        ($"{string.Format(TestMethodFormat, Member.Name)}_" +
                         $"{((MethodInfo) Member).ToParameterSignature()}").ReplaceAll(Replacements).Trim("_")
                        //  $"{((MethodInfo) Member).GetParameters().Convert(Param => $"{Param.ParameterType.Name}{(Param.ParameterType.IsArray ? "Array" : "")}").Combine("_")}" +
                        /* (((MethodInfo) Member).ReturnType == typeof(void)
                             ? ""
                             : $"_{((MethodInfo) Member).ReturnType.Name}"))
                            .ReplaceAll(Replacements)*/
                    );
                    }
                }

            return null;
            }

        #endregion

        /// <summary>
        /// Retrieves a list of Trait Values targeting members being tested.
        /// <see cref="Traits.TargetMember"/>
        /// </summary>
        public static Dictionary<MemberInfo, List<string>> GetAssemblyMemberTraits(this IEnumerable<Assembly> TestAssemblies)
            {
            var TraitGetter =
                new Func<IEnumerable<Assembly>, Dictionary<MemberInfo, List<string>>>(Assemblies =>
                    {
                    var Out = new Dictionary<MemberInfo, List<string>>();
                    Assemblies.Each(Assembly =>
                        {
                        Assembly.GetExportedTypes().Each(Type =>
                            {
                            Type.GetMethods().Each(Method =>
                                {
                                List<KeyValuePair<string, string>> Traits = TraitHelper.GetTraits(Method)
                                    .Select(Trait => Trait.Key == LCore.LUnit.Traits.TargetMember);

                                if (Traits.Count > 0)
                                    Out.Add(Method, Traits.Convert(Trait => Trait.Value));
                                });
                            });
                        });
                    return Out;
                    });

            TraitGetter = TraitGetter.Cache($"{nameof(TestExt)}.{nameof(GetAssemblyMemberTraits)}");

            return TraitGetter(TestAssemblies);
            }

        /// <summary>
        /// Collects information from a <see cref="MemberInfo"/>.
        /// </summary>
        [CanBeNull]
        public static CodeCoverageMetaData GatherCodeCoverageMetaData([CanBeNull] this MemberInfo Member, string[] CustomCommentTags)
            {
            return Member == null
                ? null
                : new CodeCoverageMetaData(Member, CustomCommentTags);
            }
        }
    }