![](../Content/LUnit-banner-small.png "")
[&lt;img align=&quot;right&quot; src=&quot;../Content/LUnit-logo-small.png&quot; alt=&quot;Logo&quot; /&gt;](../../README.md)
[Up](../LUnit.md)

### LUnit

![Type Static Class](http://b.repl.ca/v1/Type-Static%20Class-blue.png "") ![Documented 77%](http://b.repl.ca/v1/Documented-77%25-green.png "")

![Covered 0%](http://b.repl.ca/v1/Covered-0%25-red.png "")

[View Source](../Extensions/LUnit.cs#L)

###### Summary

            Provides static methods used for unit testing.
            

<table>
<thead><tr><td>Public Static Methods (5)</td>
<td></td>
<td><img src="http://b.repl.ca/v1/Total%20Code%20Lines-54-blue.png" alt="Total Code Lines 54" /></td>
<td><img src="http://b.repl.ca/v1/Total%20Documentation-80%25-green.png" alt="Total Documentation 80%" /></td>
<td><img src="http://b.repl.ca/v1/Total%20Coverage-0%25-red.png" alt="Total Coverage 0%" /></td></tr></thead>
<tr><td><h4><strong><a href="LUnit_FixParameterTypes.md" alt="">FixParameterTypes</a></strong></h4></td>
<td>   </td>
<td><a href="../Extensions/LUnit.cs#L30" alt=""><img src="http://b.repl.ca/v1/Lines%20of%20Code-11-blue.png" alt="Lines of Code 11" /></a></td>
<td><img src="http://b.repl.ca/v1/Documented-Yes-brightgreen.png" alt="Documented Yes" /></td>
<td><img src="http://b.repl.ca/v1/Covered-No-red.png" alt="Covered No" /></td></tr>
<tr><td colspan="5"><h6>public static <a href="https://msdn.microsoft.com/en-us/library/system.void.aspx" alt="">void</a> <a href="LUnit_FixParameterTypes.md" alt="">FixParameterTypes</a>(<a href="https://msdn.microsoft.com/en-us/library/system.reflection.methodinfo.aspx" alt="">MethodInfo</a> Method, <a href="https://msdn.microsoft.com/en-us/library/system.object.aspx" alt="">Object</a>[] Parameters);</h6>
</td>
</tr>
<tr><td><h4><strong><a href="LUnit_FixObject.md" alt="">FixObject</a></strong></h4></td>
<td>   </td>
<td><a href="../Extensions/LUnit.cs#L47" alt=""><img src="http://b.repl.ca/v1/Lines%20of%20Code-33-blue.png" alt="Lines of Code 33" /></a></td>
<td><img src="http://b.repl.ca/v1/Documented-No-red.png" alt="Documented No" /></td>
<td><img src="http://b.repl.ca/v1/Covered-No-red.png" alt="Covered No" /></td></tr>
<tr><td colspan="5"><h6>public static <a href="https://msdn.microsoft.com/en-us/library/system.void.aspx" alt="">void</a> <a href="LUnit_FixObject.md" alt="">FixObject</a>(<a href="https://msdn.microsoft.com/en-us/library/system.reflection.methodinfo.aspx" alt="">MethodInfo</a> SourceMethod, <a href="https://msdn.microsoft.com/en-us/library/system.type.aspx" alt="">Type</a> ObjectType, ref <a href="https://msdn.microsoft.com/en-us/library/system.object.aspx" alt="">Object</a> Obj);</h6>
</td>
</tr>
<tr><td><h4><strong><a href="LUnit_GetMethodDelegate.md" alt="">GetMethodDelegate</a></strong></h4></td>
<td>   </td>
<td></td>
<td><img src="http://b.repl.ca/v1/Documented-Yes-brightgreen.png" alt="Documented Yes" /></td>
<td><img src="http://b.repl.ca/v1/Covered-No-red.png" alt="Covered No" /></td></tr>
<tr><td colspan="5"><h6>public static <a href="https://msdn.microsoft.com/en-us/library/system.object.aspx" alt="">Object</a> <a href="LUnit_GetMethodDelegate.md" alt="">GetMethodDelegate</a>(<a href="https://msdn.microsoft.com/en-us/library/system.reflection.methodinfo.aspx" alt="">MethodInfo</a> SourceMethod, <a href="https://msdn.microsoft.com/en-us/library/system.type.aspx" alt="">Type</a> ObjectType, <a href="https://msdn.microsoft.com/en-us/library/system.string.aspx" alt="">String</a> MethodName);</h6>
</td>
</tr>
<tr><td><h4><strong><a href="LUnit_GetCheckMethod.md" alt="">GetCheckMethod</a></strong></h4></td>
<td>   </td>
<td><a href="../Extensions/LUnit.cs#L200" alt=""><img src="http://b.repl.ca/v1/Lines%20of%20Code-10-blue.png" alt="Lines of Code 10" /></a></td>
<td><img src="http://b.repl.ca/v1/Documented-Yes-brightgreen.png" alt="Documented Yes" /></td>
<td><img src="http://b.repl.ca/v1/Covered-No-red.png" alt="Covered No" /></td></tr>
<tr><td colspan="5"><h6>public static <a href="https://msdn.microsoft.com/en-us/library/bb534960.aspx" alt="" target="_blank">Func</a>&lt;<a href="https://msdn.microsoft.com/en-us/library/system.boolean.aspx" alt="">Boolean</a>&gt; <a href="LUnit_GetCheckMethod.md" alt="">GetCheckMethod</a>(<a href="https://msdn.microsoft.com/en-us/library/system.reflection.methodinfo.aspx" alt="">MethodInfo</a> SourceMethod, <a href="https://msdn.microsoft.com/en-us/library/system.string.aspx" alt="">String</a> MethodName);</h6>
</td>
</tr>
<tr><td><h4><strong><a href="LUnit_GetCheckMethodArg.md" alt="">GetCheckMethodArg</a></strong></h4></td>
<td>   </td>
<td></td>
<td><img src="http://b.repl.ca/v1/Documented-Yes-brightgreen.png" alt="Documented Yes" /></td>
<td><img src="http://b.repl.ca/v1/Covered-No-red.png" alt="Covered No" /></td></tr>
<tr><td colspan="5"><h6>public static <a href="https://msdn.microsoft.com/en-us/library/bb549151.aspx" alt="" target="_blank">Func</a>&lt;<a href="https://msdn.microsoft.com/en-us/library/system.object.aspx" alt="">Object</a>, <a href="https://msdn.microsoft.com/en-us/library/system.boolean.aspx" alt="">Boolean</a>&gt; <a href="LUnit_GetCheckMethodArg.md" alt="">GetCheckMethodArg</a>(<a href="https://msdn.microsoft.com/en-us/library/system.reflection.methodinfo.aspx" alt="">MethodInfo</a> SourceMethod, <a href="https://msdn.microsoft.com/en-us/library/system.string.aspx" alt="">String</a> MethodName);</h6>
</td>
</tr>
<tr><td width="850px" colspan="5"></td></tr>
</table>


<table>
<thead><tr><td>Public Static Classes (3)</td>
<td></td>
<td><img src="http://b.repl.ca/v1/Total%20Code%20Lines-68-blue.png" alt="Total Code Lines 68" /></td>
<td><img src="http://b.repl.ca/v1/Total%20Documentation-100%25-brightgreen.png" alt="Total Documentation 100%" /></td>
<td><img src="http://b.repl.ca/v1/Total%20Coverage-0%25-red.png" alt="Total Coverage 0%" /></td></tr></thead>
<tr><td><h4><strong><a href="LUnit_Categories.md" alt="">Categories</a></strong></h4></td>
<td>   </td>
<td><a href="../Extensions/LUnit.cs#L236" alt=""><img src="http://b.repl.ca/v1/Lines%20of%20Code-36-blue.png" alt="Lines of Code 36" /></a></td>
<td><img src="http://b.repl.ca/v1/Documented-Yes-brightgreen.png" alt="Documented Yes" /></td>
<td><img src="http://b.repl.ca/v1/Covered-No-red.png" alt="Covered No" /></td></tr>
<tr><td colspan="5"><h6></h6>
</td>
</tr>
<tr><td><h4><strong><a href="LUnit_Format.md" alt="">Format</a></strong></h4></td>
<td>   </td>
<td><a href="../Extensions/LUnit.cs#L287" alt=""><img src="http://b.repl.ca/v1/Lines%20of%20Code-20-blue.png" alt="Lines of Code 20" /></a></td>
<td><img src="http://b.repl.ca/v1/Documented-Yes-brightgreen.png" alt="Documented Yes" /></td>
<td><img src="http://b.repl.ca/v1/Covered-No-red.png" alt="Covered No" /></td></tr>
<tr><td colspan="5"><h6></h6>
</td>
</tr>
<tr><td><h4><strong><a href="LUnit_Urls.md" alt="">Urls</a></strong></h4></td>
<td>   </td>
<td><a href="../Extensions/LUnit.cs#L312" alt=""><img src="http://b.repl.ca/v1/Lines%20of%20Code-12-blue.png" alt="Lines of Code 12" /></a></td>
<td><img src="http://b.repl.ca/v1/Documented-Yes-brightgreen.png" alt="Documented Yes" /></td>
<td><img src="http://b.repl.ca/v1/Covered-No-red.png" alt="Covered No" /></td></tr>
<tr><td colspan="5"><h6></h6>
</td>
</tr>
<tr><td width="850px" colspan="5"></td></tr>
</table>




---

Copyright 2016 &copy; [Home](../../README.md) [Table of Contents](../../TableOfContents.md)

This markdown was generated by [LDoc](https://github.com/CodeSingularity/LDoc), powered by [LUnit](https://github.com/CodeSingularity/LUnit), [LCore](https://github.com/CodeSingularity/LCore)
