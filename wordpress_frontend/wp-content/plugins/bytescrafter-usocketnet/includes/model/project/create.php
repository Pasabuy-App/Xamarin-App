
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

    add_action('wp_ajax_CreateNewApp', 'CreateNewApp');
    add_action('wp_ajax_nopriv_CreateNewApp', 'CreateNewApp');
    function CreateNewApp() 
    { 
        if( !isset($_POST['appname_create']) || !isset($_POST['appdesc_create']) || !isset($_POST['appurl_create']) || !isset($_POST['appsta_create']) || !isset($_POST['appmtcap_create']) || !isset($_POST['appcap_create']) )
        {
            echo json_encode( 
                array(
                    'status'=>'danger',
                    'message'=>'All inputs is required and neccesary for project to be created.'
                ) 
            );
            wp_die();
        }

        $appname = $_POST['appname_create'];
        $appdesc = $_POST['appdesc_create'];
        $appurl = $_POST['appurl_create'];
        $appsta = $_POST['appsta_create'];
        $appmtcap = $_POST['appmtcap_create'];
        $appcap = $_POST['appcap_create'];

        global $wpdb; //Reference to wp mysql conn.
        $appsTable = USN_PROJECT_TAB;

        $appCheck = $wpdb->get_results("SELECT app_owner, wp_users.user_login FROM $appsTable, wp_users WHERE app_name = '$appname'");

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

        $generatedKey = wp_hash( wp_get_current_user()->ID . date("Y-m-d H:i:s u") );
        $data_array = array(
            'app_owner' => wp_get_current_user()->ID,
            'app_secret' => $generatedKey,
            'app_status' => $appsta,
            'app_name' => $appname,
            'app_info' => $appdesc,
            'app_website' => $appurl,
            'match_cap' => $appmtcap,
            'max_connect' => $appcap,
        );

        //Check on variant area only.
        if(isset($_POST['app_parent'])) {
            $parent_id = $_POST['app_parent'];
            $data_array['app_parent'] = $parent_id;
        }

        $result = $wpdb->insert($appsTable, $data_array, $format=NULL);

        if( $result !== FALSE ) {
            echo json_encode( 
                array(
                    'status'=>'success',
                    'message'=>'The project has been added successfully with appkey of: '.$generatedKey
                ) 
            );
        } else {
            echo json_encode( 
                array(
                    'status'=>'danger',
                    'message'=>'There was a problem on saving this project. Try different value for the name.'
                ) 
            );
        }
        wp_die();
    }

?>