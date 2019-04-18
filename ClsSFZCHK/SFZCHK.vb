' '' 18位身份证标准在国家质量技术监督局于1999年7月1日实施的GB11643-1999《公民身份号码》中做了明确的规定。 GB11643-1999《公民身份号码》为GB11643-1989《社会保障号码》的修订版，其中指出将原标准名称"社会保障号码"更名为"公民身份号码"，另外GB11643-1999《公民身份号码》从实施之日起代替GB11643-1989。GB11643-1999《公民身份号码》主要内容如下： 
' ''一、范围 
' ''该标准规定了公民身份号码的编码对象、号码的结构和表现形式，使每个编码对象获得一个唯一的、不变的法定号码。 
' ''二、编码对象 
' ''公民身份号码的编码对象是具有中华人民共和国国籍的公民。 
' ''三、号码的结构和表示形式 
' ''1、号码的结构 
' ''公民身份号码是特征组合码，由十七位数字本体码和一位校验码组成。排列顺序从左至右依次为：六位数字地址码，八位数字出生日期码，三位数字顺序码和一位数字校验码。 
' ''2、地址码 
' ''表示编码对象常住户口所在县(市、旗、区)的行政区划代码，按GB/T2260的规定执行。 
' ''3、出生日期码 
' ''表示编码对象出生的年、月、日，按GB/T7408的规定执行，年、月、日代码之间不用分隔符。 
' ''4、顺序码 
' ''表示在同一地址码所标识的区域范围内，对同年、同月、同日出生的人编定的顺序号，顺序码的奇数分配给男性，偶数分配给女性。 
' ''5、校验码 
' ''（1）十七位数字本体码加权求和公式 
' ''S = Sum(Ai * Wi), i = 0, ... , 16 ，先对前17位数字的权求和 
' ''Ai:表示第i位置上的身份证号码数字值 
' ''Wi:表示第i位置上的加权因子 
' ''Wi: 7 9 10 5 8 4 2 1 6 3 7 9 10 5 8 4 2 
' ''（2）计算模 
' ''Y = mod(S, 11) 

' ''（3）通过模得到对应的校验码 
' ''Y: 0 1 2 3 4 5 6 7 8 9 10 
' ''校验码: 1 0 X 9 8 7 6 5 4 3 2 
' ''四、举例如下： 
' ''北京市朝阳区: 11010519491231002X 
' ''广东省汕头市: 440524188001010014 


'本类实现：输入一个身份证码，检验是否有误
Public Class SFZCHK
    Private Sub New()
        '
    End Sub
    ''' <summary>
    ''' 身份证号码验证信息
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum SFZCHKInfo
        身份证号码输入正确
        身份证号码长度有误
        身份证号码日期非法
        身份证号码性别有误
        身份证号码输入有误
    End Enum
    ''' <summary>
    ''' 性别
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum SFZSex
        男
        女
        未知
    End Enum

    Shared mySex As SFZSex
    Shared mySFZID As String

    ''' <summary>
    ''' 检查身份证号码是否正确
    ''' </summary>
    ''' <param name="SFZID">要验证的身份证号码</param>
    ''' <param name="Sex">要验证的性别</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Shared Function SFZCHK(ByVal SFZID As String, ByRef Sex As SFZSex) As SFZCHKInfo
        mySex = Sex
        mySFZID = SFZID.Trim

        SFZCHK = myCHK()
        Sex = mySex
    End Function

    Private Shared Function myCHK() As SFZCHKInfo
        If mySFZID.Length <> 18 Then
            Return SFZCHKInfo.身份证号码长度有误
        End If

        Try
            Dim myDate As New System.DateTime(mySFZID.Substring(6, 4), mySFZID.Substring(10, 2), mySFZID.Substring(12, 2))
        Catch ex As Exception
            Return SFZCHKInfo.身份证号码日期非法
        End Try

        Select Case mySex
            Case SFZSex.男
                If Int(mySFZID.Substring(14, 3).Trim) Mod 2 = 0 Then
                    Return SFZCHKInfo.身份证号码性别有误
                End If
            Case SFZSex.女
                If Int(mySFZID.Substring(14, 3).Trim) Mod 2 = 1 Then
                    Return SFZCHKInfo.身份证号码性别有误
                End If
            Case Else
                If Int(mySFZID.Substring(14, 3).Trim) Mod 2 = 1 Then
                    mySex = SFZSex.男
                Else
                    mySex = SFZSex.女
                End If
        End Select

        '        校验码()
        '（1）十七位数字本体码加权求和公式 
        'S = Sum(Ai * Wi), i = 0, ... , 16 ，先对前17位数字的权求和 
        'Ai:     表示第i位置上的身份证号码数字值()
        'Wi:     表示第i位置上的加权因子()
        'Wi: 7 9 10 5 8 4 2 1 6 3 7 9 10 5 8 4 2 
        Dim W() As Integer = {7, 9, 10, 5, 8, 4, 2, 1, 6, 3, 7, 9, 10, 5, 8, 4, 2}
        Dim A() As Char = mySFZID.ToCharArray
        Dim S As Integer = 0
        Dim i As Integer
        For i = 0 To 16
            S += A(i).ToString * W(i)
        Next

        '（2）计算模 
        'Y = mod(S, 11) 
        Dim Y As Integer
        Y = S Mod 11

        '（3）通过模得到对应的校验码 
        'Y: 0 1 2 3 4 5 6 7 8 9 10 
        '校验码: 1 0 X 9 8 7 6 5 4 3 2 
        Dim CHK As String = ""
        Select Case Y
            Case 0
                CHK = "1"
            Case 1
                CHK = "0"
            Case 2
                CHK = "X"
            Case 3
                CHK = "9"
            Case 4
                CHK = "8"
            Case 5
                CHK = "7"
            Case 6
                CHK = "6"
            Case 7
                CHK = "5"
            Case 8
                CHK = "4"
            Case 9
                CHK = "3"
            Case 10
                CHK = "2"
        End Select

        If mySFZID.Substring(17, 1) = CHK Then
            Return SFZCHKInfo.身份证号码输入正确
        Else
            Return SFZCHKInfo.身份证号码输入有误
        End If

    End Function
End Class
