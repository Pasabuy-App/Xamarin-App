
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

    add_action('wp_ajax_DeleteThisCluster', 'DeleteThisCluster');
    add_action('wp_ajax_nopriv_DeleteThisCluster', 'DeleteThisCluster');
    function DeleteThisCluster() 
    { 
        if( !isset($_POST['cluster_id']) )
        {
            echo json_encode( 
                array(
                    'status'=>'danger', 
                    'message'=>'Cluster id is required during cluster deletion, contact your administrator.'
                ) 
            );
            wp_die();
        }

        global $wpdb; //Reference to wp mysql conn.
        $clusterTable = USN_CLUSTER_TAB;

        $rows = $wpdb->get_results( "DELETE FROM $clusterTable WHERE ID = ".$_POST['cluster_id']);

        if( $rows !== FALSE ) {
            echo json_encode( array('status'=>'success', 'message'=>'The cluster had been removed successfully.') );
        } else {
            echo json_encode( array('status'=>'danger', 'message'=>'There was a problem on deleting this cluster.') );
        }
        wp_die();
    }

?>