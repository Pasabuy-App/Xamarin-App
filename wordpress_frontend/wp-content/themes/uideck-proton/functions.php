<?php
	/**
	 * Basically, all the logic happens here.
	 *
	 * @package uideck-proton
	 * @since 0.1.0
	 */

     #region WP Recommendation - Prevent direct initilization of the plugin.
        if ( !defined( 'ABSPATH' ) ) { exit; } // Exit if accessed directly
        if ( ! function_exists( 'is_plugin_active' ) ) 
        {
            require_once( ABSPATH . 'wp-admin/includes/plugin.php' );
        }
    #endregion
?>

<?php

    
    /**
     * Sets up theme defaults and registers support for various WordPress features.
     *
     * Note that this function is hooked into the after_setup_theme hook, which
     * runs before the init hook. The init hook is too late for some features, such
     * as indicating support for post thumbnails.
     */
    function uideck_proton_theme_support() {

        // Add default posts and comments RSS feed links to head.
        add_theme_support( 'automatic-feed-links' );

        // Custom background color.
        add_theme_support(
            'custom-background',
            array(
                'default-color' => '#fff',
            )
        );

        // Set content-width.
        global $content_width;
        if ( ! isset( $content_width ) ) {
            $content_width = 580;
        }

        /*
        * Enable support for Post Thumbnails on posts and pages.
        *
        * @link https://developer.wordpress.org/themes/functionality/featured-images-post-thumbnails/
        */
        add_theme_support( 'post-thumbnails' );

        // Set post thumbnail size.
        set_post_thumbnail_size( 1200, 9999 );

        // Add custom image size used in Cover Template.
        add_image_size( 'uideckproton-fullscreen', 1980, 9999 );

        // Custom logo.
        $logo_width  = 120;
        $logo_height = 90;

        // If the retina setting is active, double the recommended width and height.
        if ( get_theme_mod( 'retina_logo', false ) ) {
            $logo_width  = floor( $logo_width * 2 );
            $logo_height = floor( $logo_height * 2 );
        }

        add_theme_support(
            'custom-logo',
            array(
                'height'      => $logo_height,
                'width'       => $logo_width,
                'flex-height' => true,
                'flex-width'  => true,
            )
        );

        /*
        * Let WordPress manage the document title.
        * By adding theme support, we declare that this theme does not use a
        * hard-coded <title> tag in the document head, and expect WordPress to
        * provide it for us.
        */
        add_theme_support( 'title-tag' );

        /*
        * Switch default core markup for search form, comment form, and comments
        * to output valid HTML5.
        */
        add_theme_support(
            'html5',
            array(
                'search-form',
                'comment-form',
                'comment-list',
                'gallery',
                'caption',
                'script',
                'style',
            )
        );

        /*
        * Make theme available for translation.
        * Translations can be filed in the /languages/ directory.
        * If you're building a theme based on Twenty Twenty, use a find and replace
        * to change 'twentytwenty' to the name of your theme in all the template files.
        */
        load_theme_textdomain( 'uideckproton' );

        // Add support for full and wide align images.
        add_theme_support( 'align-wide' );

        // Add support for responsive embeds.
        add_theme_support( 'responsive-embeds' );

        /*
        * Adds starter content to highlight the theme on fresh sites.
        * This is done conditionally to avoid loading the starter content on every
        * page load, as it is a one-off operation only needed once in the customizer.
        */
        // if ( is_customize_preview() ) {
        //     require get_template_directory() . '/inc/starter-content.php';
        //     add_theme_support( 'starter-content', twentytwenty_get_starter_content() );
        // }

        // Add theme support for selective refresh for widgets.
        add_theme_support( 'customize-selective-refresh-widgets' );
    }
    add_action( 'after_setup_theme', 'uideck_proton_theme_support' );

    //Include scripts that is needed js and css.
    function uid_plugin_frontend_enqueue()
    {   
        wp_enqueue_style('uid_bootstrap_css', 
            get_template_directory_uri() . '/assets/css/bootstrap.min.css', 
            array(), 
            false
        );

        wp_enqueue_style('uid_lineicons_css', 
            get_template_directory_uri() . '/assets/css/line-icons.css', 
            array(), 
            false
        );

        wp_enqueue_style('uid_owlcarousel_css', 
            get_template_directory_uri() . '/assets/css/owl.carousel.css', 
            array(), 
            false
        );

        wp_enqueue_style('uid_owltheme_css', 
            get_template_directory_uri() . '/assets/css/owl.theme.css', 
            array(), 
            false
        );

        wp_enqueue_style('uid_animate_css', 
            get_template_directory_uri() . '/assets/css/animate.css', 
            array(), 
            false
        );

        wp_enqueue_style('uid_magnificpopup_css', 
            get_template_directory_uri() . '/assets/css/magnific-popup.css', 
            array(), 
            false
        );

        wp_enqueue_style('uid_nivolightbox_css', 
            get_template_directory_uri() . '/assets/css/nivo-lightbox.css', 
            array(), 
            false
        );

        wp_enqueue_style( "uid_styles_css", get_stylesheet_uri() );

        wp_enqueue_style('uid_responsive_css', 
            get_template_directory_uri() . '/assets/css/responsive.css', 
            array(), 
            false
        );

        //ISSUE: Not working on Footer echo.
        //wp_enqueue_script('uid-jquery-js', get_template_directory_uri() . '/assets/js/jquery-min.js', array('jquery'), '2.1.4', false);
        // wp_enqueue_script('uid-popper-js', get_template_directory_uri() . '/assets/js/popper.min.js', array(''), '', false);
        // wp_enqueue_script('uid-bootstrap-js', get_template_directory_uri() . '/assets/js/bootstrap.min.js', array(''), '4.1.1', false);
        // wp_enqueue_script('uid-owlcarousel-js', get_template_directory_uri() . '/assets/js/owl.carousel.js', array(''), '1.3.3', false);
        // wp_enqueue_script('uid-jquerymixitup-js', get_template_directory_uri() . '/assets/js/jquery.mixitup.js', array(''), '2.1.11', false);
        // wp_enqueue_script('uid-jquerynav-js', get_template_directory_uri() . '/assets/js/jquery.nav.js', array(''), '3.0.0', false);
        // wp_enqueue_script('uid-scrollingnav-js', get_template_directory_uri() . '/assets/js/scrolling-nav.js', array(''), '', false);
        // wp_enqueue_script('uid-jqueryeasing-js', get_template_directory_uri() . '/assets/js/jquery.easing.min.js', array(''), '1.3', false);
        // wp_enqueue_script('uid-wow-js', get_template_directory_uri() . '/assets/js/wow.js', array(''), '', false);
        // wp_enqueue_script('uid-jquerycounterup-js', get_template_directory_uri() . '/assets/js/jquery.counterup.min.js', array(''), '1.0', false);
        // wp_enqueue_script('uid-nivolightbox-js', get_template_directory_uri() . '/assets/js/nivo-lightbox.js', array(''), '1.3.1', false);
        // wp_enqueue_script('uid-magnificpopup-js', get_template_directory_uri() . '/assets/js/jquery.magnific-popup.min.js', array(''), '1.1.0', false);
        // wp_enqueue_script('uid-waypoints-js', get_template_directory_uri() . '/assets/js/waypoints.min.js', array(''),'1.6.2', false);
        // wp_enqueue_script('uid-formvalidator-js', get_template_directory_uri() . '/assets/js/form-validator.min.js', array(''), '0.8.1', false);
        // wp_enqueue_script('uid-contactform-js', get_template_directory_uri() . '/assets/js/contact-form-script.js', array(''), '', false);
        // wp_enqueue_script('uid-script-js', get_template_directory_uri() . '/assets/js/script.js', array(''),'script', false);
    }
    add_action( 'wp_enqueue_scripts', 'uid_plugin_frontend_enqueue' );

    function my_filter_head() {
        // show admin bar only for admins and editors. 
        // if admin only, use: manage_options
        // if (!current_user_can('edit_posts')) {
        //     add_filter('show_admin_bar', '__return_false');
        //     remove_action('wp_head', '_admin_bar_bump_cb');
        // }

        // Simply check, whoever the user is and is not currently on admin?
        // Then, dont display WordPress topbar.
        if ( ! is_admin() ) {
            add_filter('show_admin_bar', '__return_false');
            remove_action('wp_head', '_admin_bar_bump_cb');
        }
    } add_action('get_header', 'my_filter_head');
