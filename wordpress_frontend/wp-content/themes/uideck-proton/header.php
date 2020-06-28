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
    <meta charset="<?php bloginfo( 'charset' ); ?>" />
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1">
    <title><?php echo get_bloginfo( 'name' ).$page_subname; ?></title>
    <?php wp_head(); ?>
  </head>

    <!-- Header Section Start -->
    <header id="home" class="hero-area-2">    
      <div class="overlay"></div>
      <nav class="navbar navbar-expand-md bg-inverse fixed-top scrolling-navbar">
        <div class="container">
          <a href="<?php echo home_url(); ?>" class="navbar-brand"><img src="<?php getCustomLogo(); ?>" alt="" style="width: 160px;"></a>  
          <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarCollapse" aria-controls="navbarCollapse" aria-expanded="false" aria-label="Toggle navigation">
            <i class="lni-menu"></i>
          </button>
          <div class="collapse navbar-collapse" id="navbarCollapse">
            <ul class="navbar-nav mr-auto w-100 justify-content-end">
              <li class="nav-item">
                <a class="nav-link page-scroll" href="#home">Home</a>
              </li>
              <li class="nav-item">
                <a class="nav-link page-scroll" href="#app-features">Features</a>
              </li>  
              <li class="nav-item">
                <a class="nav-link page-scroll" href="#screenshots">Screenshots</a>
              </li>                            
              <li class="nav-item">
                <a class="nav-link page-scroll" href="#testimonial">Testimonial</a>
              </li> 
              <li class="nav-item">
                <a class="nav-link page-scroll" href="#pricing">Plans</a>
              </li>  
              <li class="nav-item">
                <a class="nav-link page-scroll" href="#download">Download</a>
              </li> 
              <li class="nav-item">
                <a class="nav-link page-scroll" href="#map-area">Contact</a>
              </li> 
            </ul>
          </div>
        </div>
      </nav>  
      <div class="container">      
        <div class="row space-100">
          <div class="col-lg-7 col-md-12 col-xs-12">
            <div class="contents">
              <h2 class="head-title">Get Your App Landing Page <br> With Proton Template</h2>
              <p>lorem ipsum dolor sit amet, consectetur adipisicing elit. Esse unde blanditiis nostrum mollitia aliquam sed. Numquam ipsum unde repellendus similique autem non ab quibusdam enim provident distinctio! Fugit tenetur, iusto.</p>
              <div class="header-button">
                <a href="#" class="btn btn-border-filled">Learn More</a>
                <a href="#" class="btn btn-border">Get Started</a>
              </div>
            </div>
          </div>
          <div class="col-lg-5 col-md-12 col-xs-12">
            <div class="intro-img">
              <img src="<?php echo get_template_directory_uri(); ?>/assets/img/intro-mobile.png" alt="">
            </div>            
          </div>
        </div> 
      </div>             
    </header>
    <!-- Header Section End --> 