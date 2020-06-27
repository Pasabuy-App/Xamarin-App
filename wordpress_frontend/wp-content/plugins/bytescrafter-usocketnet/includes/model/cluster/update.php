
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

    add_action('wp_ajax_UpdateThisCluster', 'UpdateThisCluster');
    add_action('wp_ajax_nopriv_UpdateThisCluster', 'UpdateThisCluster');
    function UpdateThisCluster() 
    { 
        if( !isset($_POST['cluster_id']) || !isset($_POST['cluster_name'])  || !isset($_POST['cluster_info']) || !isset($_POST['cluster_hostname']) || !isset($_POST['cluster_capacity']) )
        {
            echo json_encode( 
                array(
                    'status'=>'danger', 
                    'message'=>'All inputs is required and neccesary for cluster to be updated.'
                ) 
            );
            wp_die();
        }

        $cluster_id = $_POST['cluster_id'];
        $cluster_name = $_POST['cluster_name'];
        $cluster_info = $_POST['cluster_info'];
        $cluster_hostname = $_POST['cluster_hostname'];
        $cluster_capacity = $_POST['cluster_capacity'];

        global $wpdb; //Reference to wp mysql conn.
        $clusterTable = USN_CLUSTER_TAB;

        $clusterCheckExist = $wpdb->get_results("SELECT cluster_owner, wp_users.user_login FROM $clusterTable, wp_users WHERE $clusterTable.ID != '$cluster_id' AND cluster_name = '$cluster_name'");
        if( count($clusterCheckExist) >= 1 )
        {
            echo json_encode( 
                array( 
                    'status'=>'danger',
                    'message'=>'Name of the cluster already exist owned by: ' . $clusterCheckExist[0]->user_login
                ) 
            );
            wp_die();
        }

        $updates = $wpdb->get_results( "UPDATE $clusterTable SET cluster_name = '$cluster_name', cluster_info = '$cluster_info', cluster_hostname = '$cluster_hostname', cluster_capacity = '$cluster_capacity', cluster_capacity = '$cluster_capacity' WHERE ID = '$cluster_id'" );

        if( $updates !== FALSE ) {
            echo json_encode( array('status'=>'success', 'message'=>'The cluster has been updated successfully.') );
        } else {
            echo json_encode( array('status'=>'danger', 'message'=>'There was a problem on updating this cluster.') );
        }
        wp_die();
    }

?>