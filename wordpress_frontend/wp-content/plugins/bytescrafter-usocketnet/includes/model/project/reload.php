
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

    add_action('wp_ajax_ReloadProjects', 'ReloadProjects');
    add_action('wp_ajax_nopriv_ReloadProjects', 'ReloadProjects');
    function ReloadProjects() 
    { 
        global $wpdb;
        $appsTable = USN_PROJECT_TAB;

        //SELECT ALL ENTRY on bc_apps
        $appList = $wpdb->get_results( "SELECT $appsTable.ID, app_owner, app_secret, app_status, app_name, app_info, app_website, match_cap, max_connect, date_created, wp_users.user_login FROM $appsTable, wp_users WHERE wp_users.ID = app_owner AND app_parent = 0");

        if( $appList !== FALSE ) {
            echo json_encode( array('status'=>'success', 'message'=> $appList ) );
        } else {
            echo json_encode( array('status'=>'danger', 'message'=>'There was a problem on loading project.') );
        }
        wp_die();
    }

    add_action('wp_ajax_ReloadVariants', 'ReloadVariants');
    add_action('wp_ajax_nopriv_ReloadVariants', 'ReloadVariants');
    function ReloadVariants() 
    { 
        if( !isset($_POST['app_parent']) )
        {
            echo json_encode( 
                array(
                    'status'=>'danger',
                    'message'=>'Primary project of this variant cant be found!'
                ) 
            );
            wp_die();
        }
        $parent = $_POST['app_parent'];

        global $wpdb;
        $appsTable = USN_PROJECT_TAB;

        //SELECT ALL ENTRY on bc_apps
        $appList = $wpdb->get_results( "SELECT $appsTable.ID, app_owner, app_secret, app_status, app_name, app_info, app_website, match_cap, max_connect, date_created, wp_users.user_login FROM $appsTable, wp_users WHERE wp_users.ID = app_owner AND app_parent = $parent");

        if( $appList !== FALSE ) {
            echo json_encode( array('status'=>'success', 'message'=> $appList ) );
        } else {
            echo json_encode( array('status'=>'danger', 'message'=>'There was a problem on loading project.') );
        }
        wp_die();
    }

?>