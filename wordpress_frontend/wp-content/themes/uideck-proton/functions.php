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

        // aside – Typically styled without a title. Similar to a Facebook note update.
        // gallery – A gallery of images. Post will likely contain a gallery shortcode and will have image attachments.
        // link – A link to another site. Themes may wish to use the first <a href=””> tag in the post content as the external link for that post. An alternative approach could be if the post consists only of a URL, then that will be the URL and the title (post_title) will be the name attached to the anchor for it.
        // image – A single image. The first <img /> tag in the post could be considered the image. Alternatively, if the post consists only of a URL, that will be the image URL and the title of the post (post_title) will be the title attribute for the image.
        // quote – A quotation. Probably will contain a blockquote holding the quote content. Alternatively, the quote may be just the content, with the source/author being the title.
        // status – A short status update, similar to a Twitter status update.
        // video – A single video or video playlist. The first <video /> tag or object/embed in the post content could be considered the video. Alternatively, if the post consists only of a URL, that will be the video URL. May also contain the video as an attachment to the post, if video support is enabled on the blog (like via a plugin).
        // audio – An audio file or playlist. Could be used for Podcasting.
        // chat – A chat transcript, like so:
        add_theme_support( 'post-formats', array( 'status', 'image', 'gallery' ) );

        /*
        * Enable support for Post Thumbnails on posts and pages.
        *
        * @link https://developer.wordpress.org/themes/functionality/featured-images-post-thumbnails/
        */
        add_theme_support( 'post-thumbnails' );

        // Set post thumbnail size.
        set_post_thumbnail_size( 1200, 9999 );

        // Add custom image size used in Cover Template.
        add_image_size( 'header-icon-size', 163, 50 );

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

    //Customizer class and functions.
    require get_template_directory() . '/classes/class-customizer.php';

    //Include scripts that is needed js and css.
    function uid_plugin_frontend_enqueue()
    {   
        wp_enqueue_style('uid_bootstrap_css', 
            get_template_directory_uri() . '/assets/css/bootstrap.min.css', 
            array(), 
            false
        );

        wp_enqueue_style('uid_fontawesome_css', 
            get_template_directory_uri() . '/assets/css/fontawesome-all.css', 
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

    //Sideabr widget.
    function uid_register_sidebars() {
        register_sidebar(
            array(
                'id'            => 'primary',
                'name'          => __( 'Primary Sidebar' ),
                'description'   => __( 'Put all content that you want to show in this site wide sidebar.' ),
                'before_widget' => '<div id="%1$s" class="widget %2$s">',
                'after_widget'  => '</div>',
                'before_title'  => '<h3 class="widget-title">',
                'after_title'   => '</h3>',
            )
        );
    }
    add_action( 'widgets_init', 'uid_register_sidebars' );

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
