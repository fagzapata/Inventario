<?php
  ob_start();
  require_once('includes/load.php');
  if($session->isUserLoggedIn(true)) { redirect('home.php', false);}
?>
<!DOCTYPE html>
<html lang="en">
<head>
  <meta charset="UTF-8">
  <meta name="viewport" content="width=device-width, initial-scale=1.0">
  <meta http-equiv="X-UA-Compatible" content="ie=edge">
  <link rel="stylesheet" href="libs/css/altern.css">
  <link rel="icon" type="image/png" href="images/icons/favicon.ico"/>
<!--===============================================================================================-->
<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.4/css/bootstrap.min.css"/>
<!--===============================================================================================-->
	<link rel="stylesheet" type="text/css" href="fonts/font-awesome-4.7.0/css/font-awesome.min.css">
<!--===============================================================================================-->
	<link rel="stylesheet" type="text/css" href="vendor/animate/animate.css">
<!--===============================================================================================-->	
	<link rel="stylesheet" type="text/css" href="vendor/css-hamburgers/hamburgers.min.css">
<!--===============================================================================================-->
	<link rel="stylesheet" type="text/css" href="vendor/select2/select2.min.css">
  <title>Spivsa S.A de C.V.</title>
</head>
<body>
<div class="limiter">
  <div class="container-login100">
  <div class="wrap-login100">
      <div class="login100-pic js-tilt" data-tilt>
        <img src="./uploads/users/img-01.png" alt="logos">
      </div>
      <?php echo display_msg($msg); ?>
      <form class="login100-form validate-form"  method="post" action="auth.php">
        <span class="login100-form-title">
          Inicio de sesion
        </span>
        <div class="wrap-input100 validate-input">
        <input class="input100" type="name" name="username" placeholder="Usuario">
        <span class="focus-input100"></span>
        <span class="symbol-input100">
						<i class="fas fa-user" aria-hidden="true"></i>
				</span>
        </div>
        <div class="wrap-input100 validate-input">
          <input type="password" name= "password" class="input100" placeholder="Contraseña">
          <span class="focus-input100"></span>
						<span class="symbol-input100">
            <i class="fas fa-lock" aria-hidden="true"></i>
						</span>
        </div>
        <div class="container-login100-form-btn">
        <button type="submit" class="login100-form-btn">Entrar</button>
        </div>
      </form>
    </div>
  </div>
</div>
  <!--lidbdd-->
  <script src="libs/js/tilt/tilt.jquery.min.js"></script>
	<script >
		$('.js-tilt').tilt({
			scale: 1.1
		})
  </script>
  <?php include_once('layouts/footer.php'); ?>
</body>
</html>
