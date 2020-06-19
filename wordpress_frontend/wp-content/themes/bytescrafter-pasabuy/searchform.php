    
    <?php
        /**
        * If WordPress cannot find front-page.php and “your latest posts” is 
        * set in the front page displays section, it will look for home.php. 
        *
        * @package pasabuy
        * @since 0.1.0
        */
    ?>

    <form role="search" method="get" id="searchform" action="<?php echo home_url( '/' ); ?>" style="margin-bottom: 25px;">
        <input class="form-control mr-sm-2" type="text" value="" name="s" id="s" placeholder="Search" aria-label="Search">
    </form>