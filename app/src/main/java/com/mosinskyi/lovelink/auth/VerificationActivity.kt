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
import com.mosinskyi.lovelink.activity.register.WelcomeActivity
import com.mosinskyi.lovelink.databinding.ActivityVerificationBinding

class VerificationActivity : AppCompatActivity() {

    private lateinit var binding: ActivityVerificationBinding
    private val user = FirebaseAuth.getInstance().currentUser!!

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        enableEdgeToEdge()
        binding = ActivityVerificationBinding.inflate(layoutInflater)
        setContentView(binding.root)
        ViewCompat.setOnApplyWindowInsetsListener(findViewById(R.id.main)) { v, insets ->
            val systemBars = insets.getInsets(WindowInsetsCompat.Type.systemBars())
            v.setPadding(systemBars.left, systemBars.top, systemBars.right, systemBars.bottom)
            insets
        }
        binding.checkVerificationButton.setOnClickListener {
            checkVerification()
        }
    }

    private fun checkVerification() {
        user.reload()
        if (user.isEmailVerified) {
            startActivity(Intent(this, WelcomeActivity::class.java))
        } else {
            Toast.makeText(this, "Pass the verification first", Toast.LENGTH_SHORT)
                .show()
        }
    }
}