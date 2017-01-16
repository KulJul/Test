;#DropDB TestXAFEasyTest

#Application TestXAF.Win
#Application TestXAF.Web

*FillForm
 User Name = Admin
*Action Log On

#IfDef TestXAF.Win

*Action About...
 
*Action OK

#EndIf

#IfDef TestXAF.Web
*Action Navigation(My Details)
#EndIf
