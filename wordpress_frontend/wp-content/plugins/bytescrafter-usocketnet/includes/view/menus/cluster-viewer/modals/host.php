
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

<div id="ShowMachineInfo" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
    <div class="vertical-alignment-helper">
        <div class="modal-dialog vertical-align-center" style="margin-top: 49px;">

            <div class="modal-content">

                <div class="modal-header">
                    <h4 class="modal-title" style="text-align: center;">Host Machine Info</h4>
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                </div>

                <div class="modal-body">
                    <div class="d-flex flex-column">
                        <div class="p-2">Hostname: <strong id="host-name"></strong></div>
                        <div class="p-2">Operating System: <strong id="host-system"></strong></div>
                        <div class="p-2">Architecture: <strong id="host-arch"></strong></div>
                        <div class="p-2">Processor: <strong id="host-processor"></strong></div>
                        <div class="p-2">Memory: <strong id="host-memory"></strong></div>
                        <div class="p-2">Uptime: <strong id="host-uptime"></strong></div>
                    </div>

                    <div id="host-info-notify" class="alert alert-info usn-center-item " role="alert" style="margin-top: 20px;">
                        Note: The hostname information is only availble if the host already has process running.
                    </div>
                </div>

            </div>

        </div>
    </div>
</div>