��C R E A T E   D A T A B A S E   o n l i n e _ s t o r e ;  
 G O  
 U S E   [ o n l i n e _ s t o r e ]  
 G O  
 / * * * * * *   O b j e c t :     T a b l e   [ d b o ] . [ a c t i v i t y _ h i s t o r y ]         S c r i p t   D a t e :   3 / 6 / 2 0 1 7   3 : 5 5 : 5 1   P M   * * * * * * /  
 S E T   A N S I _ N U L L S   O N  
 G O  
 S E T   Q U O T E D _ I D E N T I F I E R   O N  
 G O  
 C R E A T E   T A B L E   [ d b o ] . [ a c t i v i t y _ h i s t o r y ] (  
 	 [ i d ]   [ i n t ]   I D E N T I T Y ( 1 , 1 )   N O T   N U L L ,  
 	 [ u s e r _ i d ]   [ i n t ]   N U L L ,  
 	 [ p r o d u c t _ i d ]   [ i n t ]   N U L L ,  
 	 [ t i m e _ s t a m p ]   [ d a t e t i m e ]   N U L L ,  
 	 [ a c t i v i t y ]   [ v a r c h a r ] ( 2 5 5 )   N U L L ,  
 	 [ n o t e s ]   [ v a r c h a r ] ( 1 0 0 0 )   N U L L  
 )   O N   [ P R I M A R Y ]  
  
 G O  
 / * * * * * *   O b j e c t :     T a b l e   [ d b o ] . [ c a r t _ p r o d u c t s ]         S c r i p t   D a t e :   3 / 6 / 2 0 1 7   3 : 5 5 : 5 1   P M   * * * * * * /  
 S E T   A N S I _ N U L L S   O N  
 G O  
 S E T   Q U O T E D _ I D E N T I F I E R   O N  
 G O  
 C R E A T E   T A B L E   [ d b o ] . [ c a r t _ p r o d u c t s ] (  
 	 [ i d ]   [ i n t ]   I D E N T I T Y ( 1 , 1 )   N O T   N U L L ,  
 	 [ u s e r _ i d ]   [ i n t ]   N U L L ,  
 	 [ p r o d u c t _ i d ]   [ i n t ]   N U L L ,  
 	 [ q u a n t i t y ]   [ i n t ]   N U L L  
 )   O N   [ P R I M A R Y ]  
  
 G O  
 / * * * * * *   O b j e c t :     T a b l e   [ d b o ] . [ c a t e g o r i e s ]         S c r i p t   D a t e :   3 / 6 / 2 0 1 7   3 : 5 5 : 5 1   P M   * * * * * * /  
 S E T   A N S I _ N U L L S   O N  
 G O  
 S E T   Q U O T E D _ I D E N T I F I E R   O N  
 G O  
 C R E A T E   T A B L E   [ d b o ] . [ c a t e g o r i e s ] (  
 	 [ i d ]   [ i n t ]   I D E N T I T Y ( 1 , 1 )   N O T   N U L L ,  
 	 [ n a m e ]   [ v a r c h a r ] ( 2 5 5 )   N U L L  
 )   O N   [ P R I M A R Y ]  
  
 G O  
 / * * * * * *   O b j e c t :     T a b l e   [ d b o ] . [ p i c t u r e s ]         S c r i p t   D a t e :   3 / 6 / 2 0 1 7   3 : 5 5 : 5 1   P M   * * * * * * /  
 S E T   A N S I _ N U L L S   O N  
 G O  
 S E T   Q U O T E D _ I D E N T I F I E R   O N  
 G O  
 C R E A T E   T A B L E   [ d b o ] . [ p i c t u r e s ] (  
 	 [ i d ]   [ i n t ]   I D E N T I T Y ( 1 , 1 )   N O T   N U L L ,  
 	 [ p i c t u r e _ k e y ]   [ v a r c h a r ] ( 2 5 5 )   N U L L  
 )   O N   [ P R I M A R Y ]  
  
 G O  
 / * * * * * *   O b j e c t :     T a b l e   [ d b o ] . [ p i c t u r e s _ p r o d u c t s ]         S c r i p t   D a t e :   3 / 6 / 2 0 1 7   3 : 5 5 : 5 1   P M   * * * * * * /  
 S E T   A N S I _ N U L L S   O N  
 G O  
 S E T   Q U O T E D _ I D E N T I F I E R   O N  
 G O  
 C R E A T E   T A B L E   [ d b o ] . [ p i c t u r e s _ p r o d u c t s ] (  
 	 [ i d ]   [ i n t ]   I D E N T I T Y ( 1 , 1 )   N O T   N U L L ,  
 	 [ p r o d u c t _ i d ]   [ i n t ]   N U L L ,  
 	 [ p i c t u r e _ i d ]   [ i n t ]   N U L L  
 )   O N   [ P R I M A R Y ]  
  
 G O  
 / * * * * * *   O b j e c t :     T a b l e   [ d b o ] . [ p r o d u c t s ]         S c r i p t   D a t e :   3 / 6 / 2 0 1 7   3 : 5 5 : 5 1   P M   * * * * * * /  
 S E T   A N S I _ N U L L S   O N  
 G O  
 S E T   Q U O T E D _ I D E N T I F I E R   O N  
 G O  
 C R E A T E   T A B L E   [ d b o ] . [ p r o d u c t s ] (  
 	 [ i d ]   [ i n t ]   I D E N T I T Y ( 1 , 1 )   N O T   N U L L ,  
 	 [ n a m e ]   [ v a r c h a r ] ( 2 5 5 )   N U L L ,  
 	 [ c o u n t ]   [ i n t ]   N U L L ,  
 	 [ r a t i n g ]   [ i n t ]   N U L L ,  
 	 [ p r i c e ]   [ d e c i m a l ] ( 9 ,   2 )   N U L L ,  
 	 [ d e s c r i p t i o n ]   [ v a r c h a r ] ( 2 5 5 )   N U L L  
 )   O N   [ P R I M A R Y ]  
  
 G O  
 / * * * * * *   O b j e c t :     T a b l e   [ d b o ] . [ p r o d u c t s _ c a t e g o r i e s ]         S c r i p t   D a t e :   3 / 6 / 2 0 1 7   3 : 5 5 : 5 1   P M   * * * * * * /  
 S E T   A N S I _ N U L L S   O N  
 G O  
 S E T   Q U O T E D _ I D E N T I F I E R   O N  
 G O  
 C R E A T E   T A B L E   [ d b o ] . [ p r o d u c t s _ c a t e g o r i e s ] (  
 	 [ i d ]   [ i n t ]   I D E N T I T Y ( 1 , 1 )   N O T   N U L L ,  
 	 [ p r o d u c t _ i d ]   [ i n t ]   N U L L ,  
 	 [ c a t e g o r y _ i d ]   [ i n t ]   N U L L  
 )   O N   [ P R I M A R Y ]  
  
 G O  
 / * * * * * *   O b j e c t :     T a b l e   [ d b o ] . [ p r o f i l e s ]         S c r i p t   D a t e :   3 / 6 / 2 0 1 7   3 : 5 5 : 5 1   P M   * * * * * * /  
 S E T   A N S I _ N U L L S   O N  
 G O  
 S E T   Q U O T E D _ I D E N T I F I E R   O N  
 G O  
 C R E A T E   T A B L E   [ d b o ] . [ p r o f i l e s ] (  
 	 [ i d ]   [ i n t ]   I D E N T I T Y ( 1 , 1 )   N O T   N U L L ,  
 	 [ u s e r _ i d ]   [ i n t ]   N U L L ,  
 	 [ s t r e e t ]   [ v a r c h a r ] ( 2 5 5 )   N U L L ,  
 	 [ c i t y ]   [ v a r c h a r ] ( 2 5 5 )   N U L L ,  
 	 [ s t a t e ]   [ v a r c h a r ] ( 2 5 5 )   N U L L ,  
 	 [ z i p _ c o d e ]   [ i n t ]   N U L L ,  
 	 [ p h o n e _ n u m b e r ]   [ v a r c h a r ] ( 2 5 5 )   N U L L  
 )   O N   [ P R I M A R Y ]  
  
 G O  
 / * * * * * *   O b j e c t :     T a b l e   [ d b o ] . [ r e v i e w s ]         S c r i p t   D a t e :   3 / 6 / 2 0 1 7   3 : 5 5 : 5 1   P M   * * * * * * /  
 S E T   A N S I _ N U L L S   O N  
 G O  
 S E T   Q U O T E D _ I D E N T I F I E R   O N  
 G O  
 C R E A T E   T A B L E   [ d b o ] . [ r e v i e w s ] (  
 	 [ i d ]   [ i n t ]   I D E N T I T Y ( 1 , 1 )   N O T   N U L L ,  
 	 [ u s e r _ i d ]   [ i n t ]   N U L L ,  
 	 [ p r o d u c t _ i d ]   [ i n t ]   N U L L ,  
 	 [ r a t i n g ]   [ i n t ]   N U L L ,  
 	 [ r e v i e w _ t e x t ]   [ v a r c h a r ] ( 5 0 0 0 )   N U L L  
 )   O N   [ P R I M A R Y ]  
  
 G O  
 / * * * * * *   O b j e c t :     T a b l e   [ d b o ] . [ r e v i e w s _ p i c t u r e s ]         S c r i p t   D a t e :   3 / 6 / 2 0 1 7   3 : 5 5 : 5 1   P M   * * * * * * /  
 S E T   A N S I _ N U L L S   O N  
 G O  
 S E T   Q U O T E D _ I D E N T I F I E R   O N  
 G O  
 C R E A T E   T A B L E   [ d b o ] . [ r e v i e w s _ p i c t u r e s ] (  
 	 [ i d ]   [ i n t ]   I D E N T I T Y ( 1 , 1 )   N O T   N U L L ,  
 	 [ p i c t u r e _ i d ]   [ i n t ]   N U L L ,  
 	 [ r e v i e w _ i d ]   [ i n t ]   N U L L  
 )   O N   [ P R I M A R Y ]  
  
 G O  
 / * * * * * *   O b j e c t :     T a b l e   [ d b o ] . [ u s e r s ]         S c r i p t   D a t e :   3 / 6 / 2 0 1 7   3 : 5 5 : 5 1   P M   * * * * * * /  
 S E T   A N S I _ N U L L S   O N  
 G O  
 S E T   Q U O T E D _ I D E N T I F I E R   O N  
 G O  
 C R E A T E   T A B L E   [ d b o ] . [ u s e r s ] (  
 	 [ i d ]   [ i n t ]   I D E N T I T Y ( 1 , 1 )   N O T   N U L L ,  
 	 [ f i r s t _ n a m e ]   [ v a r c h a r ] ( 2 5 5 )   N U L L ,  
 	 [ l a s t _ n a m e ]   [ v a r c h a r ] ( 2 5 5 )   N U L L ,  
 	 [ u s e r n a m e ]   [ v a r c h a r ] ( 2 5 5 )   N U L L ,  
 	 [ p a s s w o r d ]   [ v a r c h a r ] ( 2 5 5 )   N U L L ,  
 	 [ a d m i n _ p r i v i l e g e s ]   [ b i t ]   N U L L  
 )   O N   [ P R I M A R Y ]  
  
 G O  
 
