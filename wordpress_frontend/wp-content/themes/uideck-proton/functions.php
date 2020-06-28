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
        if (!current_user_can('edit_posts')) {
            add_filter('show_admin_bar', '__return_false');
            remove_action('wp_head', '_admin_bar_bump_cb');
        }

        add_filter('show_admin_bar', '__return_false');
        remove_action('wp_head', '_admin_bar_bump_cb');
    } add_action('get_header', 'my_filter_head');
