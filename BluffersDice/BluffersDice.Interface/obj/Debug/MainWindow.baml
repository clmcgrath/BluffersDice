   M S B A M L   `   `   `  ����Q  MBluffersDice.Interface, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]3clr-namespace:BluffersDice.Interface.CustomControls%BluffersDice.Interface.CustomControls  5   	   '    !BluffersDice.Interface.MainWindow   FCustomControls3clr-namespace:BluffersDice.Interface.CustomControls  R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  Bluffer's Dice��5   	   $	��419��5   	   $��644.965��6    � WindowState$ 	Maximized=�6'    � Icon$ /res/dice/6.png��5   	   ��5	      �� 5
   
    j� TileMode$
 Tile=�6    �� ImageSource$ res/ui/Felt-Green.jpg��6%   6K    � 	Resources 5      =   7BluffersDice.Interface.CustomControls.OutlinedTextBlock �� 
TargetType �� Property �� Value��%�   '        �� 5   
   "  6   .�5      ��  �@ Stroke
 �   �6'   �� 5       �@ StrokeThickness$	 0.3Q�60   �� 5       �@ Fill
 �����6%   �� 5       �@ 
FontWeight$ SemiBold"�6+   5      5      .��5      � 	 � RowDefinitions	 5   
   �� 5      $	��60* �6   �� 5      $
��200* �6   �� 5      $	��86* �6   �� 5      $	��42* �6   �� 5      $	��37* �6   
 � ColumnDefinitions
 5   
   �� 5      $
��200* �6   �� 5       $
��300* �6   �� 5!      $
��200* �6   .+�5#   
   (� $��1��6   .��5$        -   5          Name$ lbl_RoundNumber��5$   /   $��
20,10,10,0q�6H      Text$  Round {0} ��6\   5%      (� 5&   
   $
��Left=�6   $	��Top=�6.   $��15,0,0,0q�6F   .��5'        $ Player 1��6/      TextWrapping$
 Wrap=�6?   $
��Left=�6S   $��44��6n   5(      (� 5)   
   $��2��6   $��Right=�6"   $	��Top=�6>   $��0,0,15,0q�6V   .��5*        $ Player 2��60   5+      (� 5,   
   $��1��6   $
��Left=�6   $��15,0,0,0q�6:   $	��Top=�6L   .��5-        -   5        $ lbl_Player1Score��5-   /   $ 0��6I   $
��Left=�6R      StrokeThickness$
 0.15Q�6m   5.      (� 5/   
   $��2��6   $��1��6"   $	��Top=�6/   $��Right=�6G   $
��15,0q�6c   .��50        -   5        $ lbl_Player2Score��50   /   $ 0��6I   $	 0.1Q�6R   51      (� 52   
   $��1��6   $��3��6"   .��53      ��  �� Orientation$ 
Horizontal=�6   .+�54      �� -   5        $��btn_StartTurn��54      $��	0,0,2.5,0q�6J   .��55      �� -   5        $ ��btn_lbl_TakeTurnButtonText��55   8   $��Player {0}'s Turn��6   $��5(�6Z   $��Stretch=�6g   $��Stretch=�6�   56      59      5:      (� 5<   
   -   5        $��vbox_GameOver��5<   0   $��1��6   $��1��6"   $��2��6E   .��5>      �� $��Center=�6   .+�5@      (� .��5A        $ 	Game Over��67   5B      (� 5C      .��5D        $ Winner:��67   5E      (� 5F      .��5G        -   5        $ lbl_WinnerText��5G   7   $  ��6P   5H      5I      5J      (� 5L   
   -   5        $��vbox_ClaimControl��5L   0   $��1��6   $��1��6"   $��2��6I   .��5M      �� $��	0,60,0,60q�6   $��Center=�6/   .+�5N      (� .��5O        $ 	Must Beat��67      FontSize$ 30(�6H      TextDecorations$ 	Underline{�6V   5P      �� 5R      -	   5        $��pnl_ClaimDice��5R      (� 5S      .��5T        -
   5        $ lbl_ClaimValue��5T   E   $ 20(�67   $  {Claim Value} ��6]   5U      (� 5V      $��50��6   .��5W      �� -   5        $��btn_ChallengeClaim��5W      $��0,6,0,0q�68   .��5X      �� $��Challenge Claim��6    $��5(�6;   $��Stretch=�6H   $��Stretch=�6m   5Y      5Z      5[      5]      5_      5`      