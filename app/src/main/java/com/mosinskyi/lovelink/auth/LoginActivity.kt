package com.mosinskyi.lovelink.auth

import android.content.Intent
import android.os.Bundle
import android.widget.Toast
import androidx.activity.enableEdgeToEdge
import androidx.appcompat.app.AppCompatActivity
import androidx.core.view.ViewCompat
import androidx.core.view.WindowInsetsCompat
import com.google.firebase.auth.FirebaseAuth
import com.mosinskyi.lovelink.MainActivity
import com.mosinskyi.lovelink.R
import com.mosinskyi.lovelink.databinding.ActivityLoginBinding

class LoginActivity : AppCompatActivity() {
    private lateinit var binding: ActivityLoginBinding

    private val auth = FirebaseAuth.getInstance()

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        enableEdgeToEdge()
        binding = ActivityLoginBinding.inflate(layoutInflater)
        setContentView(binding.root)
        ViewCompat.setOnApplyWindowInsetsListener(findViewById(R.id.main)) { v, insets ->
            val systemBars = insets.getInsets(WindowInsetsCompat.Type.systemBars())
            v.setPadding(systemBars.left, systemBars.top, systemBars.right, systemBars.bottom)
            insets
        }
        binding.confirmButton.setOnClickListener {
            if (binding.userEmail.editText!!.text.isEmpty()) {
                binding.userEmail.error = "Please, enter your email"
            } else {
                val userEmail = binding.userEmail.editText!!.text.toString()
                val userPassword = binding.userPassword.editText!!.text.toString()
                logIn(userEmail, userPassword)
            }
        }
        binding.newUserButton.setOnClickListener {
            startActivity(Intent(this, RegisterActivity::class.java))
        }
    }

    private fun logIn(userEmail: String, userPassword: String) {
        auth.signInWithEmailAndPassword(userEmail, userPassword)
            .addOnCompleteListener(this) { task ->
                if (task.isSuccessful) {
                    // Sign is complete
                    auth.currentUser!!.reload()
                    if (auth.currentUser!!.isEmailVerified)
                        startActivity(Intent(this, MainActivity::class.java))
                        finish()
                } else {
                    // Sign is incomplete
                    Toast.makeText(this, task.exception!!.message, Toast.LENGTH_SHORT).show()
                }
            }
    }

}