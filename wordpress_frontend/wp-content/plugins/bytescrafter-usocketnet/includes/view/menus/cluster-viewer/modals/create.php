
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

<div id="AddNewCluster" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
    <div class="vertical-alignment-helper">
        <div class="modal-dialog vertical-align-center" style="margin-top: 49px;">

            <div class="modal-content">

                <div class="modal-header">
                    <h4 class="modal-title" style="text-align: center;">Add Cluster</h4>
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                </div>

                <div class="modal-body">
                    <form id="add-cluster-form">
                        <div class="form-group">
                            <label for="cluster_name">Name:</label>
                            <input required type="text" class="form-control" id="cluster_name" name="cluster_name" placeholder="Name of this cluster.">
                        </div>
                        <div class="form-group">
                            <label for="cluster_info">Description:</label>
                            <textarea required type="text" class="form-control" id="cluster_info" name="cluster_info" rows="3"
                                placeholder="Short description of your cluster." ></textarea>
                        </div>
                        <div class="form-group">
                            <label for="cluster_hostname">Hostname:</label>
                            <input required type="text" class="form-control" id="cluster_hostname" name="cluster_hostname" placeholder="http:/www.hostname.com">
                        </div>
                        <div class="form-group">
                            <label for="cluster_capacity">Capacity:</label>
                            <input required oninput="javascript: if (this.value.length > this.maxLength) this.value = this.value.slice(0, this.maxLength);"
                                type="number" maxlength="12" class="form-control" id="cluster_capacity" name="cluster_capacity" placeholder="1000">
                        </div>
                        <div class="form-group">
                            <div class="alert alert-dark usn-center-item" role="alert">
                                <strong>NOTE:</strong> Before we submit your request a dialog confirmation will appear 
                                to ask for your permission to complete the task.
                            </div>
                        </div>
                        <div class="usn-center-item">
                            <button id="add-cluster-btn" type="submit" class="btn btn-success"> - SUBMIT - </button>
                        </div>
                        <div id="dialog-confirm-create" title="Confirmation">
                            <p id="confirm-content-create"></p>
                        </div>
                    </form>
                </div>

                <div class="modal-footer">
                    <div id="CNAMessage" class="alert usn-fullwidth usn-center-item usn-display-hide" role="alert">
                        <p id="CNAMcontent">A simple success alert—check it out!</p>
                    </div>
                </div>

            </div>

        </div>
    </div>
</div>