<?php

    /*
        Plugin Name: PasaBuy Plugin
        Plugin URI: https://www.bytescrafter.net/projects/pasabuy-plugin
        Description: WordPress plugin for PasaBuy App owned by PasaBuy Tech.
        Version: 0.1.0
        Author: Bytes Crafter
        Author URI:   https://www.bytescrafter.net/about-us
        Text Domain:  pasabuy-plugin
        
        * @package      pasabuy-plugin
        * @author       Bytes Crafter

        * @copyright    2020 Bytes Crafter
        * @version     0.1.0

        * @wordpress-plugin
        * WC requires at least: 2.5.0
        * WC tested up to: 5.4
    */

    #region WP Recommendation - Prevent direct initilization of the plugin.
        if ( !defined( 'ABSPATH' ) ) { exit; } // Exit if accessed directly
        if ( ! function_exists( 'is_plugin_active' ) ) 
        {
            require_once( ABSPATH . 'wp-admin/includes/plugin.php' );
        }
    #endregion

    //Important config files and plugin updates.
    include_once ( plugin_dir_path( __FILE__ ) . '/includes/core/config.php' );

    //Make sure to create required mysql tables.
    include_once ( plugin_dir_path( __FILE__ ) . '/includes/core/hook.php' );