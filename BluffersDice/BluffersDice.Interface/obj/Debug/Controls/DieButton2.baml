   M S B A M L   `   `   `  ����Q  MBluffersDice.Interface, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]3clr-namespace:BluffersDice.Interface.CustomControls%BluffersDice.Interface.CustomControls  5      6    0BluffersDice.Interface.CustomControls.DieButton2   FCustomControls3clr-namespace:BluffersDice.Interface.CustomControls  R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5        � UseLayoutRounding  .  � 	Resources 5	       �� 
TargetType �� Property �� Value �� Triggers �� Binding �� Value=   7BluffersDice.Interface.CustomControls.OutlinedTextBlock��%H  '��       '(� L    '  �    �� 5
   
   " ��6   .�5      ��  � ��
 ���� 6+   �� 5       � ��
 ���� 6,    5      �� 5       6   �� *IsHeld+ true69   .a�5      ��  � ��
 � ���64   5      �� 5       6   �� *IsHeld+	 false69   .a�5      ��  � ��
 ���� 64   5      5      �� 5   
   " (�6   .�5      ��  � ��$ 10q�6'   5      �� 5   
   "  6   .�5      ��  �@ Fill
 �����6%   �� 5       �@ Stroke
 �   �6'   �� 5       �@ StrokeThickness$ .3Q�60    5      �� 5        6   �� *IsHeld+ true69   .a�5!      ��  �@ Text$
 Held��6-   5"      �� 5#       6   �� *IsHeld+	 false69   .a�5$      ��  �@ Text$
 Held��6-   5%      5'      5(      .��5)      (�  J� RenderTransformOrigin$ 0.505,0.375&�6   .��5*   
   �� -   5        $��TheStackPanel��5*      .+�5+      �� -   5        $��	TheButton��5+      .��5,      �� -   5        $��img_Die��5,      ��6(   �� *Value+	 �� StringFormat$:	 4/BluffersDice.Interface;component/res/dice/{0:d}.png��5-        5.      -   5        
  Name$
 lbl_IsHeldText��5.   /      Fill
 �����   Stroke
 �   �$��20��6c      Text$  ��6r   �� 5/      -   5        $��lbl_ValueText��5/      $��15��6-   ��6<   �� *Value+50      51      52      