;#DropDB TestXafSolution2EasyTest

#Application TestXafSolution2.Win
#Application TestXafSolution2.Web

*FillForm
 User Name = Admin
*Action Log On

#IfDef TestXafSolution2.Win

*Action About...
 
*Action OK

#EndIf

#IfDef TestXafSolution2.Web
*Action Navigation(My Details)
#EndIf
