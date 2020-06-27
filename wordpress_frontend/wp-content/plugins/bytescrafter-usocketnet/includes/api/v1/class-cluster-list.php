
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
	class BC_USN_ClusterList {

		public static function initialize() {

			// STEP 1: Check if WPID and SNID is passed as this is REQUIRED!
			if (!isset($_POST["wpid"]) || !isset($_POST["snid"]) ) {
				return rest_ensure_response( 
					array(
						"status" => "unknown",
						"message" => "Please contact your administrator. Verification Unknown!",
					)
				);
			}
			$user_id = $_POST["wpid"];
            $session_token = $_POST["snid"];

			// STEP 2: Verify the Token if Valid and not expired.
			$wp_session_tokens = WP_Session_Tokens::get_instance($user_id);
			if( is_null($wp_session_tokens->get( $session_token )) ) {
				return rest_ensure_response( 
					array(
						"status" => "failed",
						"message" => "Please contact your administrator. Token Not Found!"
					)
				);
			} else {
				if( time() >= $wp_session_tokens->get( $session_token )['expiration'] )   {
					return rest_ensure_response( 
						array(
							"status" => "failed",
							"message" => "Please contact your administrator. Token Expired!"
						)
					);
				}
			}

			// STEP 3 - Get the list of cluster.
			global $wpdb; 
			$clusterTable = USN_CLUSTER_TAB;
			$clusterList = $wpdb->get_results("SELECT cluster_name, cluster_info, cluster_hostname, cluster_owner, cluster_capacity FROM $clusterTable");

			if( count($clusterList) > 0 ) {
				$newClusterList = array();
				foreach ($clusterList as $value) {
					$item = array(
						'name' => $value->cluster_name,
						'info' => $value->cluster_info,
						'host' => $value->cluster_hostname,
						'owner' => $value->cluster_owner,
						'capacity' => $value->cluster_capacity
					);
					array_push($newClusterList, $item);
				}

				return rest_ensure_response( 
					array(
						"status" => "success",
						"data" => $newClusterList
					)
				);			
			} else {
				return rest_ensure_response( 
					array(
						"status" => "unknown",
						"message" => "Cluster list is currently empty, contact administrator.",
					)
				);
			}
		}
	}

?>