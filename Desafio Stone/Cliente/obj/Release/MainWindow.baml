   M S B A M L   `   `   `  ���� B  >Cliente, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:ClienteCliente  G CClassLibrary, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullK5clr-namespace:StoneClassLibrary;assembly=ClassLibraryStoneClassLibrary     Cliente.MainWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     'localclr-namespace:Cliente     SStoneClassLibrary5clr-namespace:StoneClassLibrary;assembly=ClassLibrary     -     �Name$  Legal�� � Title$ Legal��$��390.402��$��551.063��.��� .+��� -    ��Name$ 
tabControl��$
��Left=�$	��359��$	��Top=�$	��543��.@��� -    ��Name$ TransactionTab��$��Nova transação��.��� -    �Name$ transactionGrid��$��0,0,-8,0q�.+��� -    ��Name$ amount��$
��Left=�$��23��$��	40,85,0,0q� �� TextWrapping$
 Wrap=�$	��Top=�$	��120��$��1���� -    ��Name$ 
sendButton��$��Enviar��$
��Left=�$��393,295,0,0q�$	��Top=�$��75��$��11���� -    ��Name$ label��$��Valor:��$
��Left=�$��	40,55,0,0q�$	��Top=��� -   $ 
label_Copy��$��Tipo:��$
��Left=�$��
191,55,0,0q�$	��Top=��� -	   $ 
cardNumber��$
��Left=�$��23��$��137,200,0,0q�$
 Wrap=�$	��Top=�$	��140��	 �� 	MaxLength$	 19��$��5���� -
   $ label_Copy1��$��Número:��$
��Left=�$��
16,200,0,0q�$	��Top=��� -   $ label_Copy2��$��	Bandeira:��$
��Left=�$��
16,262,0,0q�$	��Top=��� -   $ label_Copy3��$��Número de parcelas:��$
��Left=�$��
336,55,0,0q�$	��Top=��� -   
 ��Name$

 type��$
��Left=�$��
191,85,0,0q�$	��Top=�$	��120��$��2��.@��� $��Crédito���� $��Débito���� -   $
 number��$
��Left=�$��
336,85,0,0q�$	��Top=�$	��120��{�.  $��3��.@��� $��À vista���� $��2x���� $��3x���� $��4x���� $��5x���� $��6x���� -   $ label1��$��Card:��$
��Left=�$��
23,122,0,0q�$	��Top=�$��16(� J� RenderTransformOrigin$ 0.978,0.313&�$
��Bold"��� -   $ cardholderName��$
��Left=�$��23��$��137,169,0,0q�$
 Wrap=�$	��Top=�$	��140�� �� CharacterCasing$ Upper=�$��4���� -   $ label_Copy4��$��Nome:��$
��Left=�$��
16,169,0,0q�$	��Top=��� -   $ label_Copy5��$��Válido até:��$
��Left=�$��
16,231,0,0q�$	��Top=��� -   $ expirationDate��$
��Left=�$��23��$��137,231,0,0q�$
 Wrap=�$	��Top=�$	��140��$	 5��$��6���� -   $
 	cardBrand��$
��Left=�$��137,262,0,0q�$	��Top=�$	��120��$��7��.@��� $
��Visa���� $��
Mastercard���� -   $ label_Copy6��$��Senha:��$
��Left=�$��282,169,0,0q�$	��Top=��� -   $ label_Copy7��$��Tipo:��$
��Left=�$��282,200,0,0q�$	��Top=��� -   $
 cardType��$
��Left=�$��393,200,0,0q�$	��Top=�$	��120��$��9��.@��� $
��Chip���� $��Tarja magnética���� -   $ label_Copy8��$��HasPassword:��$
��Left=�$��282,231,0,0q�$	��Top=��� -    ��Name$ hasPassword��$�� ��$
��Left=�$��393,237,0,0q�$	��Top=�{�.  $��10��G� -    G�Name$ password��$
��Left=�$��393,174,0,0q�$	��Top=�$	��120�� G� 	MaxLength$ 6��$��8���� -   $ label1_Copy��$��Transaction:��$
��Left=�$��	10,14,0,0q�$	��Top=�$��20(�$ 0.978,0.313&�$
��Bold"��� -   $ TransactionsListTab��$��Minhas transações��.��� ���	#FFE5E5E5.+�&   System.Windows.Controls.DataGrid  -     Name$ dataGrid��   AutoGenerateColumns .  $
��Left=�$��	10,10,0,0q�$	��Top=�$	��303��$	��517��   CanUserAddRows .  ����    Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$
 Data��1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *date+  $
 Nome�� �� *cardholderName+  $ Número�� �� *number+  $ Valor�� �� *amount+  $
 Tipo�� �� *type+  $ Parcelas�� �� *instalments+  $ Retorno�� �� *result+