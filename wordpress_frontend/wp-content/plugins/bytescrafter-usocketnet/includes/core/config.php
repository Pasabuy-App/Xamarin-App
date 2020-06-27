
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

	DEFINE('USN_PREFIX', 'bc_usn_');

	DEFINE('USN_HOST', 'localhost');

	// Global prefix for this plugins table name prefix.
	DEFINE('USN_CLUSTER_TAB', USN_PREFIX.'clusters');

	// Global prefix for this plugins table name prefix.
	DEFINE('USN_PROJECT_TAB', USN_PREFIX.'projects');

	// Global as Plugin URL of USocketNet.
	DEFINE('USN_PLUGIN', plugin_dir_path( __FILE__ ) . '../../' );
	
	// Global as Plugin URL of USocketNet.
	DEFINE('USN_PLUGIN_URL', plugin_dir_url( __FILE__ ) . '../../' );
?>