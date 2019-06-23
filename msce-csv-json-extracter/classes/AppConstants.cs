using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*
 * Copyright (c) 2019 Lomaku Technologies.
 * All Rights Reserved.
 * 40 Estate Road, Angelo Goveya, Limbe.
 * P.O. Box 30653, Lilongwe 3.
 * +265 999 388 747
 * +265 888 676 896
 * https://github.com/LomakuIT
 * admin@lomakuit.com
 *
 * Redistribution and use in source and binary forms, with or without modification,
 * are permitted provided that the following conditions are met:
 *
 * 1. Redistributions of source code must retain the above copyright notice,
 * this list of conditions and the following disclaimer.
 *
 * 2. Redistributions in binary form must reproduce the above copyright notice,
 * this list of conditions and the following disclaimer in the documentation
 * and/or other materials provided with the distribution.
 *
 * 3. Neither the name of the copyright holder nor the names of its contributors
 * may be used to endorse or promote products derived from this software
 * without specific prior written permission.
 *
 * THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS"
 * AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO,
 * THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE
 * ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT,
 * INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO,
 * PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION)
 * HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY,
 * OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE,
 * EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE
 */
namespace msce_csv_json_extracter.classes
{/* Created by Chief Wiz on 07-06-2019
     * app constants class to keep constants, URLs, keys, TAGs, regex etc
     */
    class AppConstants
    {
        public const String REGEX_MSCE_RESULTS_2 = @"(?<candidateCode>[M][\s][0-9]{4}[\s][\/][\s][0-9]{3})[\s](?<gender>[M,F]+)[\s](?<firstname>[A-Z]+)[\s](?<surname>[A-Z,\s]+)[\s][MSCE].*(?<candidateCode1>[M][\s][0-9]{4}[\s][\/][\s][0-9]{3})[\s](?<gender1>[M,F]+)[\s](?<firstname1>[A-Z]+)[\s](?<surname1>[A-Z,\s]+)[\s][MSCE].*";
        public const String REGEX_MSCE_RESULTS_1 = @"(?<candidateCode>[M][\s][0-9]{4}[\s][\/][\s][0-9]{3})[\s](?<gender>[M,F]+)[\s](?<firstname>[A-Z]+)[\s](?<surname>[A-Z]+)[\s]+MSCE.*";
        public const String REGEX_MSCE_RESULTS_TRIMMED = "";
        public const String REGEX_MSCE_CENTRE_NAME= @".*CENTRE[\s]NO:[\s](?<centre>[A-Z,a-z,\,,(,),\\,\/,\s]+).*";
        public const String REGEX_MSCE_CENTRE_NO = @"^(?<centreNumber>[0-9]{4})$";
       // public const String REGEX_MSCE_CENTRE_NO = @"[^](?<centreNumber>[0-9]{4}$";
        public const String REGEX_MSCE_DISTRICT_NAME = @".*DISTRICT[\s]NAME:[\s](?<district>[A-Z,a-z,\s,\']+).*";
        public const String MSCE_FULL = "MALAWI SCHOOL CERTIFICATE OF EDUCATION";
        public const String TAG_CANDIDATE_CODE_GROUP = "candidateCode";
        public const String TAG_GENDER_CODE_GROUP = "gender";
        public const String TAG_FIRSTNAME_CODE_GROUP = "firstname";
        public const String TAG_SURNAME_CODE_GROUP = "surname";
        public const String TAG_CANDIDATE1_CODE_GROUP = "candidateCode1";
        public const String TAG_GENDER1_CODE_GROUP = "gender1";
        public const String TAG_FIRSTNAME1_CODE_GROUP = "firstname1";
        public const String TAG_SURNAME1_CODE_GROUP = "surname1";
        
    }
}
