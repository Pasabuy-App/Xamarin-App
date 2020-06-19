<?php
	/**
	* The template for displaying the header
	*
	* @package pasabuy
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
        <meta charset="<?php bloginfo( 'charset' ); ?>" />
        <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />
        <title><?php echo get_bloginfo( 'name' ).$page_subname; ?></title>
        <?php wp_head(); ?>
    </head>

    <body data-spy="scroll" data-target=".fixed-top">
        
        <div class="spinner-wrapper">
            <div class="spinner">
                <div class="bounce1"></div>
                <div class="bounce2"></div>
                <div class="bounce3"></div>
            </div>
        </div>        

        <!-- Navigation -->
        <nav class="navbar navbar-expand-lg navbar-dark navbar-custom fixed-top">
            <!-- Text Logo - Use this if you don't have a graphic logo -->
            <!-- <a class="navbar-brand logo-text page-scroll" href="index.html">PasaBuy</a> -->

            <!-- Image Logo -->
            <a class="navbar-brand logo-image" href="index.html"><img src="<?php echo get_template_directory_uri(); ?>/assets/images/logo.png" alt="alternative"></a>
            
            <!-- Mobile Menu Toggle Button -->
            <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarsExampleDefault" aria-controls="navbarsExampleDefault" aria-expanded="false" aria-label="Toggle navigation">
                <span class="navbar-toggler-awesome fas fa-bars"></span>
                <span class="navbar-toggler-awesome fas fa-times"></span>
            </button>
            <!-- end of mobile menu toggle button -->

            <div class="collapse navbar-collapse" id="navbarsExampleDefault">
                <ul class="navbar-nav ml-auto">
                    <li class="nav-item">
                        <a class="nav-link page-scroll" href="#header">Home <span class="sr-only">(current)</span></a>
                    </li>
                    <li class="nav-item">
                        <a class="nav-link page-scroll" href="#services">Services</a>
                    </li>
                    <li class="nav-item">
                        <a class="nav-link page-scroll" href="#pricing">Partners</a>
                    </li>
                    <li class="nav-item">
                        <a class="nav-link page-scroll" href="#request">Request</a>
                    </li>

                    <!-- Dropdown Menu -->          
                    <li class="nav-item dropdown">
                    <a class="nav-link page-scroll" href="#about" id="navbarDropdown" role="button" aria-haspopup="true" aria-expanded="false">About</a>
                        <!-- <a class="nav-link dropdown-toggle page-scroll" href="#about" id="navbarDropdown" role="button" aria-haspopup="true" aria-expanded="false">About</a>
                        <div class="dropdown-menu" aria-labelledby="navbarDropdown">
                            <a class="dropdown-item" href="#terms"><span class="item-text">Terms Conditions</span></a>
                            <div class="dropdown-items-divide-hr"></div>
                            <a class="dropdown-item" href="#privacy"><span class="item-text">Privacy Policy</span></a>
                        </div> -->
                    </li>
                    <!-- end of dropdown menu -->

                    <li class="nav-item">
                        <a class="nav-link page-scroll" href="#contact">Contact</a>
                    </li>
                </ul>
                <span class="nav-item social-icons">
                    <span class="fa-stack">
                        <a href="#your-link">
                            <i class="fas fa-circle fa-stack-2x facebook"></i>
                            <i class="fab fa-facebook-f fa-stack-1x"></i>
                        </a>
                    </span>
                    <span class="fa-stack">
                        <a href="#your-link">
                            <i class="fas fa-circle fa-stack-2x twitter"></i>
                            <i class="fab fa-twitter fa-stack-1x"></i>
                        </a>
                    </span>
                </span>
            </div>
        </nav> <!-- end of navbar -->
        <!-- end of navigation -->
