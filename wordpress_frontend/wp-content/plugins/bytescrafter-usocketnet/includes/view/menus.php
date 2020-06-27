
<?php
	// Exit if accessed directly
	if ( ! defined( 'ABSPATH' ) ) 
	{
		exit;
	}

	/** 
		* @package bytescrafter-usocketnet-restapi
		* Name: USocketNet RestAPI
		* Description: Self-Host Realtime Multiplayer Server 
		*       for your Game or Chat Application.
		* Package-Website: https://usocketnet.bytescrafter.net
		* 
		* Author: Bytes Crafter
		* Author-Website:: https://www.bytescrafter.net/about-us
		* License: Copyright (C) Bytes Crafter - All rights Reserved. 
	*/
?>

<?php

    #region Initilized new admin menu for this plugin including submenus.
        function usocketnet_init_admin_menu() 
        {
            // Add new menu to the admin page.
            add_menu_page('USocketNet', 'USocketNet', 'manage_options', 'usocketnet-getting_started', 
                'usocketnet_gettingstarted_page_callback', USN_PLUGIN_URL . '/icon.png', 4 );

            add_submenu_page('usocketnet-getting_started', 'USN Getting Started', 'Getting Started', 
                'manage_options', 'usocketnet-getting_started' );

            add_submenu_page('usocketnet-getting_started', 'USN Cluster Viewer', 'Cluster Viewer',
                'manage_options', 'usocketnet-cluster_viewer', 'usocketnet_cluster_viewer_page_callback' );

            add_submenu_page('usocketnet-getting_started', 'USN Project Browser', 'Project Browser',
                'manage_options', 'usocketnet-project_browser', 'usocketnet_project_browser_page_callback' );

            add_submenu_page('usocketnet-getting_started', 'USN Active Match', 'Active Match - Demo',
                'manage_options', 'usocketnet-active_match', 'usocketnet_active_match_page_callback' );

            add_submenu_page('usocketnet-getting_started', 'USN Online Users', 'Online Users - Demo',
               'manage_options', 'usocketnet-online_users', 'usocketnet_onlineusers_page_callback' );
 
             add_submenu_page('usocketnet-getting_started', 'USN Settings', 'Settings - Demo',
                'manage_options', 'usocketnet-settings', 'usocketnet_setting_page_callback' );
        }
        add_action('admin_menu', 'usocketnet_init_admin_menu');

        function usocketnet_gettingstarted_page_callback()
        {
            include_once( plugin_dir_path( __FILE__ ) . '/menus/getting-started.php' );
        }

        function usocketnet_cluster_viewer_page_callback()
        {
            include_once( plugin_dir_path( __FILE__ ) . '/menus/cluster-viewer.php' );
        }

        function usocketnet_project_browser_page_callback()
        {
            include_once( plugin_dir_path( __FILE__ ) . '/menus/project-browser.php' );
        }

        function usocketnet_active_match_page_callback()
        {
            include_once( plugin_dir_path( __FILE__ ) . '/menus/active-match.php' );
        }

        function usocketnet_onlineusers_page_callback()
        {
            include_once( plugin_dir_path( __FILE__ ) . '/menus/online-users.php' );
        }

        function usocketnet_setting_page_callback()
        {
            include_once( plugin_dir_path( __FILE__ ) . '/menus/settings.php' );
        }
    #endregion
?>