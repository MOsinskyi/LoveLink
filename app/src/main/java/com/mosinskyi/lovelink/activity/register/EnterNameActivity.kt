package com.mosinskyi.lovelink.activity.register

import android.content.Intent
import android.os.Bundle
import android.widget.Toast
import androidx.activity.enableEdgeToEdge
import androidx.appcompat.app.AppCompatActivity
import androidx.core.view.ViewCompat
import androidx.core.view.WindowInsetsCompat
import com.mosinskyi.lovelink.R
import com.mosinskyi.lovelink.databinding.ActivityEnterNameBinding
import com.mosinskyi.lovelink.model.UserModel

class EnterNameActivity : AppCompatActivity() {

    private lateinit var binding: ActivityEnterNameBinding

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        enableEdgeToEdge()
        binding = ActivityEnterNameBinding.inflate(layoutInflater)
        setContentView(binding.root)
        ViewCompat.setOnApplyWindowInsetsListener(findViewById(R.id.main)) { v, insets ->
            val systemBars = insets.getInsets(WindowInsetsCompat.Type.systemBars())
            v.setPadding(systemBars.left, systemBars.top, systemBars.right, systemBars.bottom)
            insets
        }

        binding.backButton.setOnClickListener {
            finish()
        }
        binding.nextButton.setOnClickListener {
            storeName()

        }

    }

    private fun storeName() {
        val userName = binding.userName.editText!!.text.toString()
        if (userName.isEmpty()) {
            Toast.makeText(this, "Please, enter your name", Toast.LENGTH_SHORT).show()
        }
        else {
            UserModel.name = userName
            startActivity(Intent(this, SelectAgeActivity::class.java))
        }


    }
}