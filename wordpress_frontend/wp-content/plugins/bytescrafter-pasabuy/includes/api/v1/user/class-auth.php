
<?php
	// Exit if accessed directly
	if ( ! defined( 'ABSPATH' ) ) 
	{
		exit;
	}

	/** 
		* @package pasabuy-plugin
		* Name: PasaBuy Plugin
		* Description: WordPress plugin for PasaBuy App owned by PasaBuy Tech.
		* Package-Website: https://www.bytescrafter.net/projects/pasabuy-plugin
		* 
		* Author: Bytes Crafter
		* Author-Website:: https://www.bytescrafter.net/about-us
		* License: Copyright (C) Bytes Crafter - All rights Reserved. 
	*/
?>

<?php

	class PSP_Authenticate {

		//Get the user session token string and if nothing, create and return one.
		public static function psp_get_session( $user_id ) {
			//Grab WP_Session_Token from wordpress.
			$wp_session_token = WP_Session_Tokens::get_instance($user_id);

			//Create a session entry unto the session tokens of user with X expiry.
			$expiration = time() + apply_filters('auth_cookie_expiration', 14 * DAY_IN_SECONDS, $user_id, true); //
			$session_now = $wp_session_token->create($expiration);
	
			return $session_now;
		}

		//Authenticate user via Rest Api.
		public static function initialize() {
		
			// Check that we're trying to authenticate
			if (!isset($_POST["UN"]) || !isset($_POST["PW"])) {
				return rest_ensure_response( 
					array(
						"status" => "unknown",
						"message" => "Please contact your administrator. Authentication Unknown!",
					)
				);
			}

			//Listens for POST values.
			$username = $_POST["UN"];
			$password = $_POST["PW"];

			//Initialize wp authentication process.
			$user = wp_authenticate($username, $password);
			
			//Check for wp authentication issue.
			if ( is_wp_error($user) ) {
				return rest_ensure_response( 
					array(
						"status" => "error",
						"message" => $user->get_error_message(),
					)
				);
			}
	
			//Return user token and id with success message.
			return rest_ensure_response( 
				array(
					"status" => "success",
					"wpid" => $user->ID,
					"snid" => PSP_Authenticate::psp_get_session($user->ID),
				)
			);
		}
	}

?>