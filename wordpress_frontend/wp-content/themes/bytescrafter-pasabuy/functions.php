<?php
	/**
	 * Basically, all the logic happens here.
	 *
	 * @package pasabuy
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

    //include post supports for formats.
    include_once( "include/override/postsupport.php" );

    //Include scripts that is needed js and css.
    function ps_plugin_frontend_enqueue()
    {    
        wp_enqueue_style('ps_google_fonts', 
            'https://fonts.googleapis.com/css?family=Raleway:400,400i,600,700,700i&amp;subset=latin-ext',
            false
        );

        wp_enqueue_style('ps_bootstrap_css', 
            get_template_directory_uri() . '/assets/css/bootstrap.css', 
            array(), 
            false
        );

        wp_enqueue_style('ps_fontawesome_css', 
            get_template_directory_uri() . '/assets/css/fontawesome-all.css', 
            array(), 
            false
        );

        wp_enqueue_style('ps_swiper_css', 
            get_template_directory_uri() . '/assets/css/swiper.css', 
            array(), 
            false
        );

        wp_enqueue_style('ps_magnific_css', 
            get_template_directory_uri() . '/assets/css/magnific-popup.css', 
            array(), 
            false
        );

        wp_enqueue_script('ps_jquery_js', get_template_directory_uri() . '/assets/js/jquery.min.js', array(),'3.3.1', false);
        wp_enqueue_script('ps_bootstrap_js', get_template_directory_uri() . '/assets/js/bootstrap.min.js', array(),'4.3.1', false);
        wp_enqueue_script('ps_popper_js', get_template_directory_uri() . '/assets/js/popper.min.js', array(),'1.0.0', false);
        wp_enqueue_script('ps_jquery-easing_js', get_template_directory_uri() . '/assets/js/jquery.easing.min.js', array(),'1.3.0', false);
        wp_enqueue_script('ps_swiper_js', get_template_directory_uri() . '/assets/js/swiper.min.js', array(),'4.4.6', false);
        wp_enqueue_script('ps_jquery-magnific-popup_js', get_template_directory_uri() . '/assets/js/jquery.magnific-popup.js', array(),'1.1.0', false);
        wp_enqueue_script('ps_validator_js', get_template_directory_uri() . '/assets/js/validator.min.js', array(),'0.11.8', false);
        wp_enqueue_script('ps_scripts_js', get_template_directory_uri() . '/assets/js/scripts.js', array(),'1.0.0', false);

        wp_enqueue_style( "ps_styles_css", get_stylesheet_uri() );
    }
    add_action( 'wp_enqueue_scripts', 'ps_plugin_frontend_enqueue' );

    // function my_filter_head() {
    //     // show admin bar only for admins and editors. 
    //     // if admin only, use: manage_options
    //     if (!current_user_can('edit_posts')) {
    //         add_filter('show_admin_bar', '__return_false');
    //         remove_action('wp_head', '_admin_bar_bump_cb');
    //     }
    // } add_action('get_header', 'my_filter_head');
    