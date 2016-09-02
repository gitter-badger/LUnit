![](../Content/LUnit-banner-small.png "")
[&lt;img align=&quot;right&quot; src=&quot;../Content/LUnit-logo-small.png&quot; alt=&quot;Logo&quot; /&gt;](../../README.md)
[Up](../LUnit.md)

### LUnit

![Type Static Class](http://b.repl.ca/v1/Type-Static%20Class-blue.png "") ![Documented 77%](http://b.repl.ca/v1/Documented-77%25-green.png "")

![Covered 0%](http://b.repl.ca/v1/Covered-0%25-red.png "")


###### Summary

            Provides static methods used for unit testing.
            

<table>
<tr><td>Public Static Methods (5)</td>
<td></td>
<td><img src="http://b.repl.ca/v1/Total%20Lines%20of%20Code-54-blue.png" alt="Total Lines of Code 54" /></td>
<td><img src="http://b.repl.ca/v1/Total%20Documented-80%25-green.png" alt="Total Documented 80%" /></td>
<td><img src="http://b.repl.ca/v1/Total%20Coverage-0%25-red.png" alt="Total Coverage 0%" /></td></tr>
<tr><td><strong><a href="LUnit_FixParameterTypes.md" alt="">FixParameterTypes</a></strong></td>
<td>   </td>
<td><a href="../Extensions/LUnit.cs#L30" alt=""><img src="http://b.repl.ca/v1/Lines%20of%20Code-11-blue.png" alt="Lines of Code 11" /></a></td>
<td><img src="http://b.repl.ca/v1/Documented-Yes-brightgreen.png" alt="Documented Yes" /></td>
<td><img src="http://b.repl.ca/v1/Covered-No-red.png" alt="Covered No" /></td></tr>
<tr><td colspan="5">public static <a href="https://msdn.microsoft.com/en-us/library/system.void.aspx" alt="">void</a> FixParameterTypes(<a href="https://msdn.microsoft.com/en-us/library/system.reflection.methodinfo.aspx" alt="">MethodInfo</a> Method, <a href="https://msdn.microsoft.com/en-us/library/system.object.aspx" alt="">Object</a>[] Parameters);</td>
</tr>
<tr><td><strong><a href="LUnit_FixObject.md" alt="">FixObject</a></strong></td>
<td>   </td>
<td><a href="../Extensions/LUnit.cs#L47" alt=""><img src="http://b.repl.ca/v1/Lines%20of%20Code-33-blue.png" alt="Lines of Code 33" /></a></td>
<td><img src="http://b.repl.ca/v1/Documented-No-red.png" alt="Documented No" /></td>
<td><img src="http://b.repl.ca/v1/Covered-No-red.png" alt="Covered No" /></td></tr>
<tr><td colspan="5">public static <a href="https://msdn.microsoft.com/en-us/library/system.void.aspx" alt="">void</a> FixObject(<a href="https://msdn.microsoft.com/en-us/library/system.reflection.methodinfo.aspx" alt="">MethodInfo</a> SourceMethod, <a href="https://msdn.microsoft.com/en-us/library/system.type.aspx" alt="">Type</a> ObjectType, ref <a href="https://msdn.microsoft.com/en-us/library/system.object.aspx" alt="">Object</a> Obj);</td>
</tr>
<tr><td><strong><a href="LUnit_GetMethodDelegate.md" alt="">GetMethodDelegate</a></strong></td>
<td>   </td>
<td><a href="../Extensions/LUnit.cs#L" alt=""><img src="http://b.repl.ca/v1/Lines%20of%20Code-0-red.png" alt="Lines of Code 0" /></a></td>
<td><img src="http://b.repl.ca/v1/Documented-Yes-brightgreen.png" alt="Documented Yes" /></td>
<td><img src="http://b.repl.ca/v1/Covered-No-red.png" alt="Covered No" /></td></tr>
<tr><td colspan="5">public static <a href="https://msdn.microsoft.com/en-us/library/system.object.aspx" alt="">Object</a> GetMethodDelegate(<a href="https://msdn.microsoft.com/en-us/library/system.reflection.methodinfo.aspx" alt="">MethodInfo</a> SourceMethod, <a href="https://msdn.microsoft.com/en-us/library/system.type.aspx" alt="">Type</a> ObjectType, <a href="https://msdn.microsoft.com/en-us/library/system.string.aspx" alt="">String</a> MethodName);</td>
</tr>
<tr><td><strong><a href="LUnit_GetCheckMethod.md" alt="">GetCheckMethod</a></strong></td>
<td>   </td>
<td><a href="../Extensions/LUnit.cs#L200" alt=""><img src="http://b.repl.ca/v1/Lines%20of%20Code-10-blue.png" alt="Lines of Code 10" /></a></td>
<td><img src="http://b.repl.ca/v1/Documented-Yes-brightgreen.png" alt="Documented Yes" /></td>
<td><img src="http://b.repl.ca/v1/Covered-No-red.png" alt="Covered No" /></td></tr>
<tr><td colspan="5">public static <a href="https://msdn.microsoft.com/en-us/library/bb534960.aspx" alt="" target="_blank">Func</a>&lt;<a href="https://msdn.microsoft.com/en-us/library/system.boolean.aspx" alt="">Boolean</a>&gt; GetCheckMethod(<a href="https://msdn.microsoft.com/en-us/library/system.reflection.methodinfo.aspx" alt="">MethodInfo</a> SourceMethod, <a href="https://msdn.microsoft.com/en-us/library/system.string.aspx" alt="">String</a> MethodName);</td>
</tr>
<tr><td><strong><a href="LUnit_GetCheckMethodArg.md" alt="">GetCheckMethodArg</a></strong></td>
<td>   </td>
<td><a href="../Extensions/LUnit.cs#L" alt=""><img src="http://b.repl.ca/v1/Lines%20of%20Code-0-red.png" alt="Lines of Code 0" /></a></td>
<td><img src="http://b.repl.ca/v1/Documented-Yes-brightgreen.png" alt="Documented Yes" /></td>
<td><img src="http://b.repl.ca/v1/Covered-No-red.png" alt="Covered No" /></td></tr>
<tr><td colspan="5">public static <a href="https://msdn.microsoft.com/en-us/library/bb549151.aspx" alt="" target="_blank">Func</a>&lt;<a href="https://msdn.microsoft.com/en-us/library/system.object.aspx" alt="">Object</a>, <a href="https://msdn.microsoft.com/en-us/library/system.boolean.aspx" alt="">Boolean</a>&gt; GetCheckMethodArg(<a href="https://msdn.microsoft.com/en-us/library/system.reflection.methodinfo.aspx" alt="">MethodInfo</a> SourceMethod, <a href="https://msdn.microsoft.com/en-us/library/system.string.aspx" alt="">String</a> MethodName);</td>
</tr>
<tr><td width="850px" colspan="360"></td></tr>
</table>


<table>
<tr><td>Public Static Abstract Classes (3)</td>
<td></td>
<td><img src="http://b.repl.ca/v1/Total%20Lines%20of%20Code-44-blue.png" alt="Total Lines of Code 44" /></td>
<td><img src="http://b.repl.ca/v1/Total%20Documented-100%25-brightgreen.png" alt="Total Documented 100%" /></td>
<td><img src="http://b.repl.ca/v1/Total%20Coverage-0%25-red.png" alt="Total Coverage 0%" /></td></tr>
<tr><td><strong><a href="LUnit_Categories.md" alt="">Categories</a></strong></td>
<td>   </td>
<td><a href="../Extensions/LUnit.cs#L236" alt=""><img src="http://b.repl.ca/v1/Lines%20of%20Code-2-blue.png" alt="Lines of Code 2" /></a></td>
<td><img src="http://b.repl.ca/v1/Documented-Yes-brightgreen.png" alt="Documented Yes" /></td>
<td><img src="http://b.repl.ca/v1/Covered-No-red.png" alt="Covered No" /></td></tr>
<tr><td colspan="5"></td>
</tr>
<tr><td><strong><a href="LUnit_Format.md" alt="">Format</a></strong></td>
<td>   </td>
<td><a href="../Extensions/LUnit.cs#L287" alt=""><img src="http://b.repl.ca/v1/Lines%20of%20Code-23-blue.png" alt="Lines of Code 23" /></a></td>
<td><img src="http://b.repl.ca/v1/Documented-Yes-brightgreen.png" alt="Documented Yes" /></td>
<td><img src="http://b.repl.ca/v1/Covered-No-red.png" alt="Covered No" /></td></tr>
<tr><td colspan="5"></td>
</tr>
<tr><td><strong><a href="LUnit_Urls.md" alt="">Urls</a></strong></td>
<td>   </td>
<td><a href="../Extensions/LUnit.cs#L312" alt=""><img src="http://b.repl.ca/v1/Lines%20of%20Code-19-blue.png" alt="Lines of Code 19" /></a></td>
<td><img src="http://b.repl.ca/v1/Documented-Yes-brightgreen.png" alt="Documented Yes" /></td>
<td><img src="http://b.repl.ca/v1/Covered-No-red.png" alt="Covered No" /></td></tr>
<tr><td colspan="5"></td>
</tr>
<tr><td width="850px" colspan="377"></td></tr>
</table>




---

Copyright 2016 &copy; [Home](../../README.md) [Table of Contents](../../TableOfContents.md)

This markdown was generated by [LDoc](https://github.com/CodeSingularity/LDoc), powered by [LUnit](https://github.com/CodeSingularity/LUnit), [LCore](https://github.com/CodeSingularity/LCore)
