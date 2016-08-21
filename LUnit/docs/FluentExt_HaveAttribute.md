###FluentExt
#HaveAttribute
####Static Method
######public static FluentAssertions.AndConstraint<TypeAssertions> HaveAttribute(TypeAssertions Type, String Because, Object[] BecauseArgs);
######Summary

            Asserts that the current System.Type has an attribute of type .
            
######Parameters
==__Add parameter type link__==
Parameter | Optional | Type | Description
 ---  |  ---  |  ---  |  --- 
Type | No | TypeAssertions | 
Because | Yes | String | A formatted phrase as is supported by System.String.Format(System.String,System.Object[])
                explaining why the assertion is needed. If the phrase does not start with the
                word because, it is prepended automatically.
            
BecauseArgs | No | Object[] | Zero or more objects to format using the placeholders in because.
####Returns
==__Add return type link__==
######AndConstraint<TypeAssertions>
An FluentAssertions.AndConstraint`1 which can be used to chain assertions.
==__source link__==
==__coverage link__==
==__exception comments__==
==__permission comments__==
==__root link__==
==__footer__==
