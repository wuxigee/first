' '' 18λ���֤��׼�ڹ������������ල����1999��7��1��ʵʩ��GB11643-1999��������ݺ��롷��������ȷ�Ĺ涨�� GB11643-1999��������ݺ��롷ΪGB11643-1989����ᱣ�Ϻ��롷���޶��棬����ָ����ԭ��׼����"��ᱣ�Ϻ���"����Ϊ"������ݺ���"������GB11643-1999��������ݺ��롷��ʵʩ֮�������GB11643-1989��GB11643-1999��������ݺ��롷��Ҫ�������£� 
' ''һ����Χ 
' ''�ñ�׼�涨�˹�����ݺ���ı�����󡢺���Ľṹ�ͱ�����ʽ��ʹÿ�����������һ��Ψһ�ġ�����ķ������롣 
' ''����������� 
' ''������ݺ���ı�������Ǿ����л����񹲺͹������Ĺ��� 
' ''��������Ľṹ�ͱ�ʾ��ʽ 
' ''1������Ľṹ 
' ''������ݺ�������������룬��ʮ��λ���ֱ������һλУ������ɡ�����˳�������������Ϊ����λ���ֵ�ַ�룬��λ���ֳ��������룬��λ����˳�����һλ����У���롣 
' ''2����ַ�� 
' ''��ʾ�������ס����������(�С��졢��)�������������룬��GB/T2260�Ĺ涨ִ�С� 
' ''3������������ 
' ''��ʾ�������������ꡢ�¡��գ���GB/T7408�Ĺ涨ִ�У��ꡢ�¡��մ���֮�䲻�÷ָ����� 
' ''4��˳���� 
' ''��ʾ��ͬһ��ַ������ʶ������Χ�ڣ���ͬ�ꡢͬ�¡�ͬ�ճ������˱ඨ��˳��ţ�˳�����������������ԣ�ż�������Ů�ԡ� 
' ''5��У���� 
' ''��1��ʮ��λ���ֱ������Ȩ��͹�ʽ 
' ''S = Sum(Ai * Wi), i = 0, ... , 16 ���ȶ�ǰ17λ���ֵ�Ȩ��� 
' ''Ai:��ʾ��iλ���ϵ����֤��������ֵ 
' ''Wi:��ʾ��iλ���ϵļ�Ȩ���� 
' ''Wi: 7 9 10 5 8 4 2 1 6 3 7 9 10 5 8 4 2 
' ''��2������ģ 
' ''Y = mod(S, 11) 

' ''��3��ͨ��ģ�õ���Ӧ��У���� 
' ''Y: 0 1 2 3 4 5 6 7 8 9 10 
' ''У����: 1 0 X 9 8 7 6 5 4 3 2 
' ''�ġ��������£� 
' ''�����г�����: 11010519491231002X 
' ''�㶫ʡ��ͷ��: 440524188001010014 


'����ʵ�֣�����һ�����֤�룬�����Ƿ�����
Public Class SFZCHK
    Private Sub New()
        '
    End Sub
    ''' <summary>
    ''' ���֤������֤��Ϣ
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum SFZCHKInfo
        ���֤����������ȷ
        ���֤���볤������
        ���֤�������ڷǷ�
        ���֤�����Ա�����
        ���֤������������
    End Enum
    ''' <summary>
    ''' �Ա�
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum SFZSex
        ��
        Ů
        δ֪
    End Enum

    Shared mySex As SFZSex
    Shared mySFZID As String

    ''' <summary>
    ''' ������֤�����Ƿ���ȷ
    ''' </summary>
    ''' <param name="SFZID">Ҫ��֤�����֤����</param>
    ''' <param name="Sex">Ҫ��֤���Ա�</param>
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
            Return SFZCHKInfo.���֤���볤������
        End If

        Try
            Dim myDate As New System.DateTime(mySFZID.Substring(6, 4), mySFZID.Substring(10, 2), mySFZID.Substring(12, 2))
        Catch ex As Exception
            Return SFZCHKInfo.���֤�������ڷǷ�
        End Try

        Select Case mySex
            Case SFZSex.��
                If Int(mySFZID.Substring(14, 3).Trim) Mod 2 = 0 Then
                    Return SFZCHKInfo.���֤�����Ա�����
                End If
            Case SFZSex.Ů
                If Int(mySFZID.Substring(14, 3).Trim) Mod 2 = 1 Then
                    Return SFZCHKInfo.���֤�����Ա�����
                End If
            Case Else
                If Int(mySFZID.Substring(14, 3).Trim) Mod 2 = 1 Then
                    mySex = SFZSex.��
                Else
                    mySex = SFZSex.Ů
                End If
        End Select

        '        У����()
        '��1��ʮ��λ���ֱ������Ȩ��͹�ʽ 
        'S = Sum(Ai * Wi), i = 0, ... , 16 ���ȶ�ǰ17λ���ֵ�Ȩ��� 
        'Ai:     ��ʾ��iλ���ϵ����֤��������ֵ()
        'Wi:     ��ʾ��iλ���ϵļ�Ȩ����()
        'Wi: 7 9 10 5 8 4 2 1 6 3 7 9 10 5 8 4 2 
        Dim W() As Integer = {7, 9, 10, 5, 8, 4, 2, 1, 6, 3, 7, 9, 10, 5, 8, 4, 2}
        Dim A() As Char = mySFZID.ToCharArray
        Dim S As Integer = 0
        Dim i As Integer
        For i = 0 To 16
            S += A(i).ToString * W(i)
        Next

        '��2������ģ 
        'Y = mod(S, 11) 
        Dim Y As Integer
        Y = S Mod 11

        '��3��ͨ��ģ�õ���Ӧ��У���� 
        'Y: 0 1 2 3 4 5 6 7 8 9 10 
        'У����: 1 0 X 9 8 7 6 5 4 3 2 
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
            Return SFZCHKInfo.���֤����������ȷ
        Else
            Return SFZCHKInfo.���֤������������
        End If

    End Function
End Class
