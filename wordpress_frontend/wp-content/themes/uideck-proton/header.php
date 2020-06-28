<?php
	/**
	* The template for displaying the header
	*
	* @package uideck-proton
	* @since 0.1.0
	*/
?>

<?php 
    $page_subname = '';
    if( is_home() ) {
        //$page_subname = ' - ' + getThemeField('blog_header', 'Change this Blog Page name on your theme Customizer');
    } else if(is_search() ) {
        //$page_subname = ' - ' + getThemeField('search_header', 'Change this Search Page name on your theme Customizer');
    } else if(is_404()) {
        //$page_subname = ' - ' + getThemeField('404_header', 'Change this 404 Page name on your theme Customizer');
    } else if(is_category()) {
        //$page_subname = ' - ' + getThemeField('category_header', 'Change this Category Page name on your theme Customizer');
    } else {
        //$page_subname = ' - ' + get_the_title(get_the_ID()); 
    } //add tag, date, category,. etc. then sidebar.
?>

<!DOCTYPE html>
<html <?php language_attributes(); ?>>
  <head>
    <!-- Required meta tags -->
    <meta charset="<?php bloginfo( 'charset' ); ?>" />
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1">
    <title><?php echo get_bloginfo( 'name' ).$page_subname; ?></title>
    <?php wp_head(); ?>

    <!-- Bootstrap CSS -->
    <!-- 
    <link rel="stylesheet" href="css/bootstrap.min.css">
    <link rel="stylesheet" href="css/line-icons.css">
    <link rel="stylesheet" href="css/owl.carousel.css">
    <link rel="stylesheet" href="css/owl.theme.css">
    <link rel="stylesheet" href="css/animate.css">
    <link rel="stylesheet" href="css/magnific-popup.css">
    <link rel="stylesheet" href="css/nivo-lightbox.css">
    <link rel="stylesheet" href="css/main.css">    
    <link rel="stylesheet" href="css/responsive.css">
    -->
  </head>