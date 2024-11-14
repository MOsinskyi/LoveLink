package com.mosinskyi.lovelink.auth

import android.content.Intent
import android.os.Bundle
import android.widget.Toast
import androidx.activity.enableEdgeToEdge
import androidx.appcompat.app.AppCompatActivity
import androidx.core.view.ViewCompat
import androidx.core.view.WindowInsetsCompat
import com.google.firebase.auth.FirebaseAuth
import com.mosinskyi.lovelink.R
import com.mosinskyi.lovelink.activity.VerificationActivity
import com.mosinskyi.lovelink.activity.WelcomeActivity
import com.mosinskyi.lovelink.databinding.ActivityRegisterBinding

class RegisterActivity : AppCompatActivity() {

    private lateinit var binding: ActivityRegisterBinding

    private val auth = FirebaseAuth.getInstance()

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        enableEdgeToEdge()
        binding = ActivityRegisterBinding.inflate(layoutInflater)
        setContentView(binding.root)
        ViewCompat.setOnApplyWindowInsetsListener(findViewById(R.id.main)) { v, insets ->
            val systemBars = insets.getInsets(WindowInsetsCompat.Type.systemBars())
            v.setPadding(systemBars.left, systemBars.top, systemBars.right, systemBars.bottom)
            insets
        }
        binding.confirmButton.setOnClickListener {
            if (binding.newUserEmail.editText!!.text.isEmpty()) {
                binding.newUserEmail.error = "Please, enter your real email"
            } else {
                val userEmail = binding.newUserEmail.editText!!.text.toString()
                val userPassword = binding.newUserPassword.editText!!.text.toString()
                auth.createUserWithEmailAndPassword(userEmail, userPassword)
                    .addOnCompleteListener(this) { task ->
                        if (task.isSuccessful) {
                            // User created successfully
                            auth.currentUser!!.sendEmailVerification()
                                .addOnCompleteListener(this) { task1 ->
                                    if (task1.isSuccessful) {
                                        startActivity(
                                            Intent(
                                                this,
                                                VerificationActivity::class.java
                                            )
                                        )
                                    }
                                    else {
                                        startActivity(
                                            Intent(
                                                this,
                                                WelcomeActivity::class.java
                                            )
                                        )
                                    }
                                    finish()
                                }
                        } else {
                            // User didn't created
                            Toast.makeText(this, task.exception!!.message, Toast.LENGTH_SHORT)
                                .show()
                        }
                    }
            }
        }
    }
}