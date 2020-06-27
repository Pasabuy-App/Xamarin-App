
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
	class BC_USN_ClusterVerify {

		public static function initialize() {

			// STEP 1: Check if WPID and SNID is passed as this is REQUIRED!
			if ( !isset($_POST["securekey"]) ) {
				return rest_ensure_response( 
					array(
						"status" => "unknown",
						"message" => "Please contact your administrator. Verification Unknown!",
					)
				);
			}
			$securekey = $_POST["securekey"];

			global $wpdb; //Reference to wp mysql conn.
            $clusterTable = USN_CLUSTER_TAB;

            $clusterCheck = $wpdb->get_results("SELECT ID, cluster_name, cluster_info, cluster_capacity FROM $clusterTable WHERE cluster_secretkey = '$securekey'");


            if( count($clusterCheck) >= 1 ) {
                return rest_ensure_response( 
                    array( 
                        'status' => 'success',
                        'data' => array(
							"clid" => $clusterCheck[0]->ID,
							"name" => $clusterCheck[0]->cluster_name,
							"info" => $clusterCheck[0]->cluster_info,
							"capacity" => $clusterCheck[0]->cluster_capacity
						)
                    ) 
                );
            } else {
                return rest_ensure_response( 
                    array( 
                        'status'=>'error',
                        'message'=>'Cluster secret key: ' . $securekey . ' is unknown!'
                    ) 
                );
            }

		}
	}

?>