
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
	class BC_USN_ProjectVerify {

		public static function initialize() {

			// STEP 1: Check if WPID and SNID is passed as this is REQUIRED!
			if (!isset($_POST["wpid"]) || !isset($_POST["snid"]) || !isset($_POST["pkey"]) ) {
				return rest_ensure_response( 
					array(
						"status" => "unknown",
						"message" => "Please contact your administrator. Verification Unknown!",
					)
				);
			}
			$user_id = $_POST["wpid"];
            $session_token = $_POST["snid"];
            $prj_key = $_POST["pkey"];

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

			// STEP 3 - Verify the AppKey Secret if valid.
			global $wpdb; 
			$appsTable = USN_PROJECT_TAB;
			$checkName = $wpdb->get_results("SELECT ID, app_name, app_info, app_website, app_status, app_parent, match_cap, max_connect FROM $appsTable WHERE app_secret = '$prj_key'");

			if( count($checkName) >= 1 ) {
				if( $checkName[0]->app_status == "Active" ) {

					return rest_ensure_response( 
						array(
							"status" => "success",
							"data" => array(
								"pjid" => $checkName[0]->ID,
								"name" => $checkName[0]->app_name,
								"desc" => $checkName[0]->app_info,
								"url" => $checkName[0]->app_website,
								"status" => $checkName[0]->app_status,
								"matchcap" => $checkName[0]->match_cap,
								"capacity" => $checkName[0]->max_connect,
								"parent" => $checkName[0]->app_parent,
							)
						)
					);

				} else {
					return rest_ensure_response( 
						array(
							"status" => "inactive",
							"message" => "Please contact your administrator. Inactive App!",
						)
					);
				}				
			} else {
				return rest_ensure_response( 
					array(
						"status" => "unknown",
						"message" => "Please contact your administrator. Secret Incorrect!",
					)
				);
			}
		}
	}

?>