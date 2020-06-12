
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
	// Global as Plugin URL of USocketNet.
	DEFINE('PSP_PLUGIN', plugin_dir_path( __FILE__ ) . '../../' );
	
	// Global as Plugin URL of USocketNet.
	DEFINE('PSP_PLUGIN_URL', plugin_dir_url( __FILE__ ) . '../../' );
?>