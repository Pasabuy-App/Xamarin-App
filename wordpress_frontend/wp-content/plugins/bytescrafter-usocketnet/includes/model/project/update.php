
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

    add_action('wp_ajax_UpdateThisApp', 'UpdateThisApp');
    add_action('wp_ajax_nopriv_UpdateThisApp', 'UpdateThisApp');
    function UpdateThisApp() 
    { 
        if( !isset($_POST['appid_edit']) || !isset($_POST['appname_edit'])  || !isset($_POST['appdesc_edit']) || !isset($_POST['appurl_edit']) || !isset($_POST['appsta_edit']) || !isset($_POST['appmtcap_edit']) || !isset($_POST['appcap_edit']))
        {
            echo json_encode( 
                array(
                    'status'=>'danger', 
                    'message'=>'All inputs is required and neccesary for project to be updated.'
                ) 
            );
            wp_die();
        }

        $appid = $_POST['appid_edit'];
        $appname = $_POST['appname_edit'];
        $appdesc = $_POST['appdesc_edit'];
        $appurl = $_POST['appurl_edit'];
        $appsta = $_POST['appsta_edit'];
        $appmtcap = $_POST['appmtcap_edit'];
        $appcap = $_POST['appcap_edit'];

        global $wpdb; //Reference to wp mysql conn.
        $appsTable = USN_PROJECT_TAB;

        $appCheck = $wpdb->get_results("SELECT app_owner, wp_users.user_login FROM $appsTable, wp_users WHERE $appsTable.ID != '$appid' AND app_name = '$appname'");
        if( count($appCheck) >= 1 )
        {
            echo json_encode( 
                array( 
                    'status'=>'danger',
                    'message'=>'Name of the project already exist owned by: ' . $appCheck[0]->user_login
                ) 
            );
            wp_die();
        }

        $updates = $wpdb->get_results( "UPDATE $appsTable SET app_name = '$appname', app_info = '$appdesc', app_website = '$appurl', app_status = '$appsta', match_cap = '$appmtcap', max_connect = '$appcap' WHERE ID = '$appid'" );
        if( $updates !== FALSE ) {
            echo json_encode( array('status'=>'success', 'message'=>'The project has been updated successfully.') );
        } else {
            echo json_encode( array('status'=>'danger', 'message'=>'There was a problem on updating this project.') );
        }
        wp_die();
    }

?>