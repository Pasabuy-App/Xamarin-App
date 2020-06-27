
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

    add_action('wp_ajax_ReloadClusterList', 'ReloadClusterList');
    add_action('wp_ajax_nopriv_ReloadClusterList', 'ReloadClusterList');
    function ReloadClusterList() 
    { 
        global $wpdb;
        $clusterTab = USN_CLUSTER_TAB;

        $clusterList = $wpdb->get_results( "SELECT $clusterTab.ID, cluster_name, cluster_info, cluster_owner, cluster_hostname, cluster_secretkey, cluster_capacity, date_created, wp_users.user_login FROM $clusterTab, wp_users WHERE wp_users.ID = cluster_owner");

        if( $clusterList !== FALSE ) {
            echo json_encode( array( 'status'=>'success', 'message'=> $clusterList ) );
        } else {
            echo json_encode( array('status'=>'danger', 'message'=>'There was a problem on loading clusters.') );
        }
        wp_die();
    }

?>