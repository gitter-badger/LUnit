###FluentExt
#HaveAttribute
####Static Method

![Documented Yes](http://b.repl.ca/v1/Documented-Yes-brightgreen.png) | ![Unit Tested No](http://b.repl.ca/v1/Unit%20Tested-No-grey.png) | ![Attribute Tests 0](http://b.repl.ca/v1/Attribute%20Tests-0-grey.png)

######public static FluentAssertions.AndConstraint<TypeAssertions> HaveAttribute(TypeAssertions Type, String Because, Object[] BecauseArgs);
######Summary

            Asserts that the current System.Type has an attribute of type .
            
######Parameters
==__Add parameter type link__==

Parameter | Optional | Type | Description
:---  | :---  | :---  | :--- 
Type | No | TypeAssertions | 
Because | Yes | String | A formatted phrase as is supported by System.String.Format(System.String,System.Object[])
                explaining why the assertion is needed. If the phrase does not start with the
                word because, it is prepended automatically.
            
BecauseArgs | No | Object[] | Zero or more objects to format using the placeholders in because.

####Returns
==__Add return type link__==
######AndConstraint<TypeAssertions>
An FluentAssertions.AndConstraint`1 which can be used to chain assertions.
==source link==
==coverage link==
==exception comments==
==permission comments==
==root link==
==footer==
