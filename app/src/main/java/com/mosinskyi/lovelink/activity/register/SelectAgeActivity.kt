package com.mosinskyi.lovelink.activity.register

import android.annotation.SuppressLint
import android.app.DatePickerDialog
import android.os.Bundle
import androidx.activity.enableEdgeToEdge
import androidx.appcompat.app.AppCompatActivity
import androidx.core.view.ViewCompat
import androidx.core.view.WindowInsetsCompat
import com.mosinskyi.lovelink.R
import com.mosinskyi.lovelink.databinding.ActivitySelectAgeBinding
import java.text.SimpleDateFormat
import java.util.Calendar
import java.util.Locale
import com.mosinskyi.lovelink.model.UserModel

class SelectAgeActivity : AppCompatActivity() {

    private lateinit var binding: ActivitySelectAgeBinding

    private val calendar = Calendar.getInstance()
    private var age: Int = 16

    @SuppressLint("SetTextI18n")
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        enableEdgeToEdge()
        binding = ActivitySelectAgeBinding.inflate(layoutInflater)
        setContentView(binding.root)
        ViewCompat.setOnApplyWindowInsetsListener(findViewById(R.id.main)) { v, insets ->
            val systemBars = insets.getInsets(WindowInsetsCompat.Type.systemBars())
            v.setPadding(systemBars.left, systemBars.top, systemBars.right, systemBars.bottom)
            insets
        }
        binding.currentAgeText.text = "Вам $age років?"
        binding.backButton.setOnClickListener {
            finish()
        }
        binding.datePickerButton.setOnClickListener {
            showDatePicker()
        }
        binding.nextButton.setOnClickListener {
            storeAge()
        }
    }

    private fun showDatePicker() {
        val datePickerDialog = DatePickerDialog(
            this, { DatePicker, year, month, dayOfMonth ->
                val selectedDate = Calendar.getInstance()
                selectedDate.set(year, month, dayOfMonth)
                val dateFormat = SimpleDateFormat("dd/MM/yyyy", Locale.getDefault())
                val formatedDate = dateFormat.format(selectedDate.time)
                binding.datePickerButton.text = formatedDate
                calculateAge(selectedDate)
            },
            calendar.get(Calendar.YEAR),
            calendar.get(Calendar.MONTH),
            calendar.get(Calendar.DAY_OF_MONTH)
        )

        // Set min date from user 16 years old
        val minDate = Calendar.getInstance()
        minDate.add(Calendar.YEAR, -16)
        datePickerDialog.datePicker.maxDate = minDate.timeInMillis

        datePickerDialog.show()
    }

    private fun calculateAge(selectedDate: Calendar) {
        val currentDate = Calendar.getInstance()
        age = currentDate.get(Calendar.YEAR) - selectedDate.get(Calendar.YEAR)
        var displayAge = "Вам $age"
        displayAge += if (age <= 20)
            " років?"
        else if (age % 10 == 1)
            " рік"
        else if (age % 10 < 5)
            " роки?"
        else
            " років?"

        binding.currentAgeText.text = displayAge

    }

    private fun storeAge() {
        UserModel.age = age.toString()
    }
}