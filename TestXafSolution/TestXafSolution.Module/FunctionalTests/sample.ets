;#DropDB TestXafSolutionEasyTest

#Application TestXafSolution.Win
#Application TestXafSolution.Web

*FillForm
 User Name = Admin
*Action Log On

#IfDef TestXafSolution.Win

*Action About...
 
*Action OK

#EndIf

#IfDef TestXafSolution.Web
*Action Navigation(My Details)
#EndIf
