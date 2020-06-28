<?php
	/**
    * If WordPress cannot find front-page.php and “your latest posts” is 
    * set in the front page displays section, it will look for home.php. 
    * Additionally, WordPress will look for this file when the posts page 
    * is set in the front page displays section.
	*
	* @package uideck-proton
	* @since 0.1.0
	*/
?>

<?php get_header(); ?>

    <?php  include_once( "blocks/features.php" ); ?>
    <?php  include_once( "assets/app.php" ); ?>

<?php get_footer(); ?>