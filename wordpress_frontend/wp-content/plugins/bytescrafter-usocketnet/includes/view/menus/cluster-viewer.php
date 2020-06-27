
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

	<?php /* Header Section */ ?>
		<div class="usn-welcome-header">
			<h1>CLUSTER VIEWER</h1>
            <p>  
                If a USocketNet is pointed to WordPress site and has an admin credential will be automatically be added here.
			</p>
		</div>
    <?php /* Header Section */ ?>
	
	<div class="usn-panel-body">

		<?php if( !isset($_GET['host']) ) { ?>

			<div class="usn-panel-first">
				<button id="ReloadClusterList" type="button" class="btn btn-dark">Refresh List</button>
				<button type="button" class="btn btn-dark" data-toggle="modal" data-target="#AddNewCluster">Add Cluster</button>
			</div>
			<table id="project-datatables" class="stripe" style="width: 100%;"></table>
			<div id="project-notification" class="alert alert-info usn-center-item " role="alert" style="margin-top: 20px;">
				Currently fetching updates for all available clusters. Please wait...
			</div>
			<?php include_once( plugin_dir_path( __FILE__ ) . "/cluster-viewer/modals/create.php" ); ?>
			<?php include_once( plugin_dir_path( __FILE__ ) . "/cluster-viewer/modals/edit.php" ); ?>
			<?php include_once( plugin_dir_path( __FILE__ ) . "/cluster-viewer/datatables.php" ); ?>

		<?php } else { ?>
			
			<div class="usn-panel-first">
				<button id="host-info-btn" type="button" class="btn btn-primary disabled" data-toggle="modal" data-target="#ShowMachineInfo">Host Machine Info</button>
				<button type="button" class="btn btn-danger" onclick="window.location.href = '<?php echo get_home_url()."/wp-admin/admin.php?page=".$_GET['page']; ?>';" >Disconnect</button>
			</div>

			<?php include_once( plugin_dir_path( __FILE__ ) . "/cluster-viewer/usocketnet.php" ); ?>
			<div class="usn-cluster-body container">
				<ul id="usn-cluster-viewer" class="row">
					<?php //include_once( plugin_dir_path( __FILE__ ) . "/cluster-viewer/_core.php" ); ?>
					<script id="processTemplate" type="text/x-handlebars-template">
						{{#each process}}
						<div id="pid-{{pid}}" class="process-single">
							<li class="col-sm-12">
								<div class="row">
									<div id="usn-instance-cpu" class="col-lg-3 usn-instance-divs" >
										<canvas id="cpu-{{pid}}" style="width: 100%; height: 200px;"></canvas>
										<h5 style="text-align: center; color: #7b7b7b;">CPU USAGE</h5>
									</div>
									<div id="usn-instance-mem" class="col-lg-3 usn-instance-divs">
										<canvas id="ram-{{pid}}" style="width: 100%; height: 200px;"></canvas>
										<h5 style="text-align: center; color: #7b7b7b;">RAM USAGE</h5>
									</div>
									<div id="usn-instance-net" class="col-lg-3 usn-instance-divs">
										<canvas id="net-{{pid}}" style="width: 100%; height: 200px;"></canvas>
										<h5 style="text-align: center; color: #7b7b7b;">EVENT LOOP</h5>
									</div>	
									<div id="usn-instance-usr" class="col-lg-3 usn-instance-divs">
										<div class="process-head" style="text-align: center;">
											<p id="name-{{pid}}" class="process-detail float-block-left ">name: <strong>{{name}}</strong></p>
											{{{processStatus pid status}}}
										</div>
										<div class="process-info">
											<p>
												<strong style="display: block;">UPTIME: </strong>
													<a id="uptime-{{pid}}">{{uptimeDisplay uptime}}</a> 
											</p>
											<p>
												<strong>RESTART: </strong><a id="ping-{{pid}}">{{restart_unstable}} / {{restart_normal}}</a>
												<strong>ERROR: </strong><a id="error-{{pid}}">0</a>x
											</p>
										</div>
										<div class="process-action">
											<div class="btn-group" role="group" aria-label="Process Action">
												{{#ifEquals status "online"}}
												<button type="button" class="btn btn-danger disabled">Stop</button>
												<button type="button" class="btn btn-info disabled">Logs</button>
												<button type="button" class="btn btn-warning disabled" onclick="restartPid({{pid}})">Restart</button>
												{{/ifEquals}}
												{{#ifEquals status "stopped"}}
												<button type="button" class="btn btn-success disabled">Start</button>
												<button type="button" class="btn btn-info disabled">Logs</button>
												<button type="button" class="btn btn-warning disabled">Reset</button>
												{{/ifEquals}}
											</div>
										</div>
										
										
									</div>
								</div>
							</li>
						</div>
						{{/each}}
					</script>
				</ul>
			</div>
			<?php include_once( plugin_dir_path( __FILE__ ) . "/cluster-viewer/modals/host.php" ); ?>

		<?php } ?>
	</div>
